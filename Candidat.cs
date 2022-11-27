using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    /// <summary>
    /// Indique le niveau de la prestation d'un candidat
    /// </summary>
    enum NiveauCandidat
    {
        NIVEAU_NUL,
        NIVEAU_MAUVAIS,
        NIVEAU_PASSABLE,
        NIVEAU_BON,
        NIVEAU_EXCELLENT,
        NBRE_NIVEAUX
    }

    internal class Candidat
    {
        /// <summary>
        /// Constructeur vide
        /// </summary>
        public Candidat() {
            ID = NbreCandidats;
            NbreCandidats++;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="nom">Nom du candidat</param>
        /// <param name="prenom">Prénom du candidat</param>
        public Candidat(String? nom, String? prenom) {
            Nom = nom;
            Prenom = prenom;
            ID = NbreCandidats;
            NbreCandidats++;
        }

        /// <summary>
        /// Nombre total de candidats
        /// </summary>
        public static uint NbreCandidats { get; set; } = 1;

        /// <summary>
        /// Nombre de passages totaux, classés par niveau de la prestation (NiveauCandidat)
        /// </summary>
        public static int[] NombrePassages { get; set; } = new int[(int)NiveauCandidat.NBRE_NIVEAUX];

        /// <summary>
        /// Identifiant du candidat
        /// </summary>
        public uint ID { get; set; }

        /// <summary>
        /// Nom de famille du candidat
        /// </summary>
        public String? Nom { get; set; }

        /// <summary>
        /// Prénom du candidat
        /// </summary>
        public String? Prenom { get; set; }

        /// <summary>
        /// Liste des passages du candidat
        /// </summary>
        public List<Passage>? ListePassages { get; set; } = new();
    }

    /// <summary>
    /// Epreuve d'un candidat
    /// </summary>
    internal class Passage
    {
        /// <summary>
        /// Constructeur vide
        /// </summary>
        public Passage() { 
            nbreBonnesReponses = 0;
            nbreQuestions = 0;
        }

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="bonnesRep">Nombre de bonnes réponses</param>
        /// <param name="totalQuest">?mbre total de questions</param>
        public Passage(int bonnesRep, int totalQuest)
        {
            nbreBonnesReponses = bonnesRep;
            nbreQuestions = totalQuest;
            Niveau = ClasserCandidat(nbreBonnesReponses, nbreQuestions);
        }

        /// <summary>
        /// Etiquettes qualifiant la prestation
        /// </summary>
        public static string[]? performanceCandidat { get; } = new string[(int)NiveauCandidat.NBRE_NIVEAUX] { "nulle", "mauvaise", "passable", "bonne", "excellente" };

        /// <summary>
        /// Etiquettes décrivant les bandes de niveau
        /// </summary>
        public static string[]? performanceSpan { get; } = new string[(int)NiveauCandidat.NBRE_NIVEAUX] { "0%-20%", "20%-40%", "40%-60%", "60%-80%", "80%-100%" };

        /// <summary>
        /// Couleurs associés aux niveaux
        /// </summary>
        public static ConsoleColor[]? performanceCandidatCouleur { get; } = new ConsoleColor[(int)NiveauCandidat.NBRE_NIVEAUX] { ConsoleColor.Red, ConsoleColor.Magenta, ConsoleColor.Yellow, ConsoleColor.Blue, ConsoleColor.Green };

        /// <summary>
        /// Classe une prestation donnée
        /// </summary>
        /// <param name="nbreBonneRep">Nombre de bonnes réponses</param>
        /// <param name="nbreQuestions">Nombre total de questions</param>
        /// <returns>Niveau de la prestation</returns>
        public static NiveauCandidat ClasserCandidat(int nbreBonneRep, int nbreQuestions)
        {
            NiveauCandidat S = NiveauCandidat.NBRE_NIVEAUX;

            if ((double)nbreBonneRep / nbreQuestions <= 0.2) S = NiveauCandidat.NIVEAU_NUL;
            else if ((double)nbreBonneRep / nbreQuestions <= 0.4) S = NiveauCandidat.NIVEAU_MAUVAIS;
            else if ((double)nbreBonneRep / nbreQuestions <= 0.6) S = NiveauCandidat.NIVEAU_PASSABLE;
            else if ((double)nbreBonneRep / nbreQuestions <= 0.8) S = NiveauCandidat.NIVEAU_BON;
            else S = NiveauCandidat.NIVEAU_EXCELLENT;

            return S;
        }

        /// <summary>
        /// Nombre de questions lors de l'épreuve
        /// </summary>
        public int nbreQuestions { get; set; }

        /// <summary>
        /// Nombre de bonnes réponses
        /// </summary>
        public int nbreBonnesReponses { get; set; }

        /// <summary>
        /// Niveau de la prestation
        /// </summary>
        public NiveauCandidat Niveau { get; set; }
    }
}
