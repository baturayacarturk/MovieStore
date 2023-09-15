using Core.CrossCuttingConcerns.Security.EncryptPrimaryKey;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonUseForTests
{
    public class EncryptionServiceWrapper 
    {
        public virtual string Encrypt(int id)
        {
            
            return EncryptionService.Encrypt(id);
        }
        public virtual  int Decrypt(string id) 
        {
            return EncryptionService.Decrypt(id);   
        }
    }

}
