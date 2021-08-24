using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Encryption
{
    public class DiffieHellman
    { 
        byte[] sharedKey(string privkey)
        {
            byte[] tmp = Encoding.UTF8.GetBytes(privkey);
            byte[] privk = Curve25519.ClampPrivateKey(tmp);
            byte[] pubk = Curve25519.GetPublicKey(privk);

            byte[] res = null;
            return res;
        }
    }
}

