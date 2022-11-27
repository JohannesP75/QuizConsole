using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    internal class Question
    {
        /// <summary>
        /// Constructeur vide
        /// </summary>
        public Question()
        {
            reponses = new string[4];
            enonce = "";
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="en">Enoncé</param>
        /// <param name="r1">Réponse 1</param>
        /// <param name="r2">Réponse 2</param>
        /// <param name="r3">Réponse 3</param>
        /// <param name="r4">Réponse 4</param>
        /// <param name="reponse">Numéro de la réponse correcte</param>
        public Question(string en, string r1, string r2, string r3, string r4, int reponse) {
            enonce = en;
            bonneReponse = false;
            m_repCorrecte = reponse;
            reponses = new string[4] { r1, r2, r3, r4 };
            //reponses = { r1, r2, r3, r4 };
            /*reponses[0] = r1;
            reponses[1] = r2;
            reponses[2] = r3;
            reponses[3] = r4;*/
        }

        /// <summary>
        /// Reçoit le numéro de la réponse du candidat
        /// </summary>
        /// <param name="answer">Réponse du candidat (entre 1 et 4)</param>
        public void Repondre(int answer)
        {
            bonneReponse = (answer == repCorrecte);
        }

        /// <summary>
        /// Enoncé de la question
        /// </summary>
        public string enonce { get; set; }

        /// <summary>
        /// Enoncés des réponses à la question
        /// </summary>
        public string[] reponses { get; set; }

        /// <summary>
        /// Indique la réponse correcte
        /// </summary>
        private int m_repCorrecte;

        /// <summary>
        /// Indique la réponse correcte
        /// </summary>
        public int repCorrecte {
            get { return m_repCorrecte; } 
            set {
                //repCorrecte = (value <= 4) ? ((value > 0) ? 1 : value) : 4;
                m_repCorrecte = (value <= 4 && value >= 1) ? value : 1;
            } 
        }

        /// <summary>
        /// Indique si on a bien répondu à la question
        /// </summary>
        public bool bonneReponse { get; set; }
    }
}
