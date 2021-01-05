using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicatonProcess.December2020.Data.Entities
{
    public abstract class Entity<PK>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public PK Id { get; set; }
        public DateTime DateCreated { get; protected set; }
        public DateTime DateModified { get; protected set; }

        public Entity()
        {
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
    }
}
