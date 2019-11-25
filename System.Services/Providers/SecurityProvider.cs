using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SYF.Data;
using SYF.Infrastructure.Configuration;
using SYF.Infrastructure.Models.Requests;
using SYF.Infrastructure.Providers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SYF.Infrastructure.Models;
using System.IdentityModel.Tokens.Jwt;
using SYF.Framework;
using System.Linq;

namespace SYF.Services.Providers
{
    public class SecurityProvider : ISecurityProvider
    {
        public SecurityProvider(DataContext context, IOptions<SystemOptions> options)
        {
            DataContext = context;
            Options = options.Value;
        }

        private DataContext DataContext { get; }
        private SystemOptions Options { get; }

        public async Task<ClaimsPrincipal> CreatePrincipalAsync(Guid userId)
        {
            var user =  await DataContext.Users
                .Include(x => x.Person)
                .FirstOrDefaultAsync( x => x.Id == userId); 

            if (user == null) return null;

            var request = new SecurityTokenRequest
            {
                Id = user.Id,
                Name = user.Person.Name,
                PersonId = user.PersonId,
                SiteId = user.SiteId
                //Roles = user.Person.
            };

            var identity = CreateIdentity(request);

            return new ClaimsPrincipal(identity);
        }

        public ClaimsPrincipal CreateSystemPrincipal(Guid siteId)
        {
            var request = new SecurityTokenRequest
            {
                Name = "system",
                SiteId = siteId
            };

            var identity = CreateIdentity(request);

            return new ClaimsPrincipal(identity);
        }

        public string CreateToken(SecurityTokenRequest request)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var identity = CreateIdentity(request);

            var symmetricKey = Options.Security.SecretKey;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,

                Issuer = Options.Security.Issuer,
                IssuedAt = DateTime.UtcNow,
                NotBefore = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddSeconds(request.Duration ?? Options.Security.Duration),

                // Create Signing Credentials 
                // Param 1 : signing key
                // Param 2 : signature algorithm
                // Param 3 : digest algorithm
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(symmetricKey)), Options.Security.Algorithm)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public string UserApiToString(UserApiModel model)
        {
            if (model.ApiKey == null) return null;

            // Ensure that the API key is at least 16 bytes
            var apiKeySize = model.ApiKey.Length;
            if (apiKeySize < 16) return null;

            var data = new byte[16 + apiKeySize];
            Array.Copy(model.Id.ToByteArray(), 0, data, 0, 16);
            Array.Copy(model.ApiKey, 0, data, 16, apiKeySize);

            return Base64UrlEncoder.Encode(data);
        }

        public UserApiModel UserApiFromString(string value)
        {
            // Ensure that the API Key Size is at least 16 bytes
            if (string.IsNullOrEmpty(value)) return null;

            var data = Base64UrlEncoder.DecodeBytes(value);
            var apiKeySize = data.Length - 16;
            if (apiKeySize < 16) return null;

            var idValue = new byte[16];
            Array.Copy(data, 0, idValue, 0, 16);

            var apiValue = new byte[apiKeySize];
            Array.Copy(data, 16, apiValue, 0, apiKeySize);

            return new UserApiModel
            {
                Id = new Guid(idValue),
                ApiKey = apiValue
            };
        }

        public bool CheckSuperUserPassword(string password)
        {
            // Check super user password options
            if (string.IsNullOrEmpty(password)) return false;
            if (string.IsNullOrEmpty(Options?.Security?.SuperUserPassword)) return false;
            if (string.IsNullOrEmpty(Options?.Security?.SuperUserPasswordSalt)) return false;

            var checkPassword = PasswordHelper.EncryptPassword(password, Options.Security.SuperUserPasswordSalt);
            return Options.Security.SuperUserPassword == checkPassword;
        }

        private ClaimsIdentity CreateIdentity(SecurityTokenRequest request)
        {
            var claims = new List<Claim>();

            if (!string.IsNullOrEmpty(request.Name))
            {
                claims.Add(new Claim(ClaimTypes.Name, request.Name, ClaimValueTypes.String));
            }

            if (request.Id != null)
            {
                claims.Add(new Claim("userId", request.Id.ToString(), ClaimValueTypes.String));
            }

            if (request.PersonId != null)
            {
                claims.Add(new Claim("personId", request.PersonId.ToString(), ClaimValueTypes.String));
            }

            if (request.SiteId != null)
            {
                claims.Add(new Claim("siteId", request.SiteId.ToString(), ClaimValueTypes.String));
            }

            if (request.SuperUser)
            {
                claims.Add(new Claim("superUser", request.SuperUser.ToString(), ClaimValueTypes.Boolean));
            }

            if (request.Roles != null)
            {
                foreach (var role in request.Roles)
                {
                    claims.Add(new Claim("roleId", role.ToString(), ClaimValueTypes.String));
                }
            }
            return new ClaimsIdentity(claims, "PIPware");
        }

        public string EncryptValue(string value, string iv)
        {
            using (var aesAlgorithm = Aes.Create())
            {
                //aesAlgorithm.Padding = PaddingMode.ANSIX923;
                aesAlgorithm.Key = Encoding.Unicode.GetBytes(Options.Security.SecretKey).Take(16).ToArray();
                aesAlgorithm.IV = Encoding.Unicode.GetBytes(iv).Take(16).ToArray();

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlgorithm.CreateEncryptor(aesAlgorithm.Key, aesAlgorithm.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(value);
                        }
                        return Convert.ToBase64String(msEncrypt.ToArray());
                    }
                }
            }
        }

        public string DecryptValue(string value, string iv)
        {
            try
            {
                using (var aesAlgorithm = Aes.Create())
                {
                    //aesAlgorithm.Padding = PaddingMode.ANSIX923;
                    aesAlgorithm.Key = Encoding.Unicode.GetBytes(Options.Security.SecretKey).Take(16).ToArray();
                    aesAlgorithm.IV = Encoding.Unicode.GetBytes(iv).Take(16).ToArray();

                    // Create an encryptor to perform the stream transform.
                    ICryptoTransform decryptor = aesAlgorithm.CreateDecryptor(aesAlgorithm.Key, aesAlgorithm.IV);

                    // Create the streams used for decryption.
                    using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(value)))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                            {
                                // Read the decrypted bytes from the decrypting stream
                                // and place them in a string.
                                return srDecrypt.ReadToEnd();
                            }
                        }
                    }
                }
            }
            // Value is either not encrypted or corrupt - return the raw value...
            catch
            {
                return value;
            }
        }
    }
}

