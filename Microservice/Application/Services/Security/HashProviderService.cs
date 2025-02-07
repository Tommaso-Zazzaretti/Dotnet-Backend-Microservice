﻿using System.Security.Cryptography;
using Microservice.Application.Services.Security.Interfaces;

namespace Microservice.Application.Services.Security
{
    /*
        SHA512 is a cryptographic hash function, while Rfc2898DeriveBytes is a key-derivation function.
        Hash functions are too fast and can be brute-forced too easily, 
        So key-derivation function introduce a cost factor to slow down the execution of a brute force attack.
        In the future it is possible to increase the number of iterations (cost factor) to cope with increasing 
        computing powers.

        Hashing password using salt is one of the best practices in protecting user accounts from hackers.
        In case hackers have stolen databases, they also need more time to decryte them. It won't be easy at all. 
        At the same time, you have time to reset all passwords or suggest users to change passwords right away.
    */
    public class HashProviderService : IHashProviderService
    {
        private readonly int _saltCharSize; // 16    => 128 bit 
        private readonly int _keyCharSize;  // 32    => 256 bit
        private readonly int _iterations;   // 10000 => Cost factor

        //Constructor via appsettings.json
        public HashProviderService(IConfiguration configuration) {
            this._keyCharSize  = configuration.GetValue<int>("HashSettings:KEY_SIZE");
            this._saltCharSize = configuration.GetValue<int>("HashSettings:SALT_SIZE");
            this._iterations   = configuration.GetValue<int>("HashSettings:ITERATIONS");
        }

        //Constructor via parameters
        public HashProviderService(int keySize, int saltSize, int iterations)
        {
            this._saltCharSize = saltSize;
            this._keyCharSize  = keySize;
            this._iterations   = iterations;
        }

        // EX: "PASSWORD"  ===>  "salt.hashedKey"
        public string Hash(string clearText)
        {
            var Algorithm = new Rfc2898DeriveBytes(clearText, _saltCharSize, _iterations, HashAlgorithmName.SHA512);
            string? Salt = Convert.ToBase64String(Algorithm.Salt);
            string HashedKey = Convert.ToBase64String(Algorithm.GetBytes(_keyCharSize));
            return $"{Salt}.{HashedKey}";
        }


        //'StoredField' is a "salt.hash" string stored in a DB, while 'Field' is the string sent in clear text by a user
        public bool Check(string StoredField, string Field)
        {
            //Null check
            if (string.IsNullOrEmpty(StoredField)) { throw new ArgumentNullException(nameof(StoredField)); }
            if (string.IsNullOrEmpty(Field))       { throw new ArgumentNullException(nameof(Field));   }
            //Extract  [SALT . HASHED_KEY]
            string[] splittedStoredField = StoredField.Split('.', 2);
            if (splittedStoredField.Length != 2) { throw new FormatException("Wrong hash format. Should be {salt}.{hash}"); }
            byte[] StoredSalt = Convert.FromBase64String(splittedStoredField[0]);
            byte[] StoredHash = Convert.FromBase64String(splittedStoredField[1]);
            //Try to hash the current Field and then compare the 2 hashes
            var Algorithm = new Rfc2898DeriveBytes(Field, StoredSalt, _iterations, HashAlgorithmName.SHA512);
            var PasswordHash = Algorithm.GetBytes(this._keyCharSize);
            return StoredHash.SequenceEqual(PasswordHash);
        }
    }
}

