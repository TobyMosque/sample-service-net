using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TobyMosque.Sample.Service.Net.Extensions
{
    public static class UserExtensions
    {
        public static void RegisterPassword(this DataEntities.User user, string password)
        {
            var pepper = user.UserID.ToByteArray().Sum(x => x);
            var salt = new byte[16];
            var random = RandomNumberGenerator.Create();
            random.GetBytes(salt);

            user.Salt = salt;
            user.Password = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, 8000 + pepper, 64);
        }

        public static bool VerifyPassword(this DataEntities.User user, string password)
        {
            var pepper = user.UserID.ToByteArray().Sum(x => x);
            var binary = KeyDerivation.Pbkdf2(password, user.Salt, KeyDerivationPrf.HMACSHA512, 8000 + pepper, 64);
            return user.Password.SequenceEqual(binary);
        }

        public static DataEntities.Session CreateSession(this DataEntities.User user)
        {
            var token = new byte[64];
            RandomNumberGenerator.Create().GetBytes(token);

            var session = new DataEntities.Session();
            session.SessionID = Guid.NewGuid();
            session.UserID = user.UserID;
            session.TenantID = user.TenantID;
            session.Token = token;
            session.IsActive = true;
            session.CreationDate = DateTime.UtcNow;
            return session;
        }
    }
}