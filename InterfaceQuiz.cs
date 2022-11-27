using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    internal abstract class InterfaceQuiz
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public InterfaceQuiz()
        {
                
        }

        /// <summary>
        /// Affiche l'interface graphisue associée
        /// </summary>
        public abstract void Run();
    }

    internal class InterfaceCandidat: InterfaceQuiz
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public InterfaceCandidat() : base() { }

        public override void Run()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Candidat
        /// </summary>
        public Candidat? Candidat { get; set; }
    }

    internal class InterfaceAdmin : InterfaceQuiz
    {
        /// <summary>
        /// Constructeur
        /// </summary>
        public InterfaceAdmin() : base() { }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
