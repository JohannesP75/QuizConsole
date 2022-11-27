using Quiz;
using System.Text.Json;

class Program
{
    static List<Question>? quests;
    static Dictionary<uint, Candidat>? ListeCandidats;

    static void Main()
    {
        // Initialisation des questions
        quests = new List<Question>();
        ListeCandidats = new Dictionary<uint, Candidat>();
        string jsonString = File.ReadAllText("questions.json");
        quests = JsonSerializer.Deserialize<List<Question>>(jsonString);
        /*
        quests.Add(new Question("En quelle année fut lancé ce language ?", "1998", "2005", "1995", "2002", 4));
        quests.Add(new Question("Quel est le type principal de programmation du C# ?", "Orienté objet", "Fonctionnel", "Impérative", "Par contrat", 1));
        quests.Add(new Question("Le framework .NET est-il compatible avec tous les langages ?", "Seulement C#", "C# et Visual BASIC", "Tout langage disposant d'un compilateur produisant du code MSIL", "Aucun", 3));
        quests.Add(new Question("Quel calendrier n'est pas inclus dans le standard .NET", "Coréen", "Taiwanais", "Hébreu", "Aztèque", 4));
        quests.Add(new Question("Quelle est la différence entre les champs const et readonly ?", "Le premier est initialisé lors de la compilation et le second lors de l'exécution", "Aucune", "La différence n'existe que dans le cadre de la programmation embarquée", "Le premier concerne les attributs d'une classe et le second les autres variables", 1));
        quests.Add(new Question("Peut-on directement manipuler la mémoire ?", "Non, le code ne s'exécutant que dans une machine virtuelle", "Oui, sans aucune limite", "Seulement dans des blocs de code 'unsafe'", "Uniquement dans les versions les plus récentes", 3));
        quests.Add(new Question(".NET possède-il des outils pour communiquer avec des serveurs mail ?", "Oui", "Seulement pour SMTP", "Seulement pour POP", "Non", 2));
        quests.Add(new Question("L'héritage multiple est-il autorisé en C# ?", "Non", "Oui, dans tous les cas", "Oui, dans certains cas", "L'héritage multiple n'existe pas", 1));
        quests.Add(new Question("Le C# est un langage...", "Interprété", "Compilé", "Précompilé", "Préinterprété", 3));
        quests.Add(new Question("Qui a dirigé la conception du C# ?", "Anders Hejlsberg", "Mads Torgersen", "Steve Jobs", "James Gosling", 1));
        */

        int choix;
        Console.WriteLine("Bienvenue dans notre quiz !");
        string? input;

        do {
            Console.WriteLine("Etes vous :\n\t(1) Candidat\n\t(2) Administrateur\n\n((0) Quitter)");
            input = Console.ReadLine();
            if (!Int32.TryParse(input, out choix)) choix = -1;

            switch (choix)
            {
                case 0:
                    Console.WriteLine("Au revoir.");
                    break;
                case 1:
                    CandidatQCM();
                    break;
                case 2:
                    AdminQCM();
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine("\tVous devez entrer un nombre !");
                    Console.ResetColor();
                    break;
            }
        } while (choix != 0);
    }

    // Fonctions pour administration
    static void listerQuestions()
    {
        Console.Clear();

        Console.WriteLine("\nVoici la liste des questions :");
        for (int i = 0; i < quests.Count; i++)
        {
            Console.WriteLine($"Question {i + 1} - {quests[i].enonce}");

            for (int j = 0; j < 4; j++) Console.WriteLine($"\t\t{j + 1} - {quests[i].reponses[j]}");
            Console.WriteLine($"La bonne réponse est la {quests[i].repCorrecte}");
        }

        Console.WriteLine("Appuyez sur une touche...");
        Console.ReadKey();
        Console.Clear();
    }

    static void ajouterQuestions()
    {
        Console.Clear();
        Console.WriteLine($"Vous allez ajouter une nouvelle question.\nQuel est l'intitulé de la nouvelle question ?");
        String? input = Console.ReadLine();
        Question nouvQuest = new();
        nouvQuest.enonce = input;

        for (int i = 0; i < 4; i++)
        {
            Console.WriteLine($"Quel est la réponse {i + 1} ?");
            input = Console.ReadLine();
            nouvQuest.reponses[i] = input;
        }

        Console.WriteLine($"Quel est la réponse correcte (entre 1 et 4) ?");
        input = Console.ReadLine();
        int newQRep;
        if (!Int32.TryParse(input, out newQRep)) newQRep = 0;
        nouvQuest.repCorrecte = newQRep;
        quests.Add(nouvQuest);

        Console.WriteLine($"Question ajoutée.\nAppuyez sur une touche...");
        Console.ReadKey();
        Console.Clear();
    }
    static void modifierQuestion()
    {
        Console.Clear();
        Console.WriteLine($"Quelle question voulez-vous modifier (prenez un nombre de 1 à {quests.Count}, et 0 pour annuler)?");
        String? input = Console.ReadLine();
        int quesMod = 0;
        if (!Int32.TryParse(input, out quesMod)) quesMod = 0;

        if (quesMod >= 1 || quesMod <= quests.Count)
        {
            Console.WriteLine($"Quel est le nouvel intitulé ?");
            input = Console.ReadLine();
            quests[quesMod - 1].enonce = input;

            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine($"Quel est la nouvelle question {i + 1} ?");
                input = Console.ReadLine();
                quests[quesMod - 1].reponses[i] = input;
            }

            Console.WriteLine($"Quel est la nouvelle réponse correcte (entre 1 et 4) ?");
            input = Console.ReadLine();
            int newRep;
            if (!Int32.TryParse(input, out newRep)) newRep = 0;
            quests[quesMod - 1].repCorrecte = newRep;

            Console.WriteLine($"Question {quesMod} modifiée.");
        }
        else if (quesMod != 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Vous deviez entrer un nombre enre 1 et {quests.Count} !");
            Console.ResetColor();
        }
        else Console.WriteLine("Decision annulée.");

        Console.WriteLine($"Appuyez sur une touche...");
        Console.ReadKey();
        Console.Clear();
    }

    static void supprimerQuestion()
    {
        Console.Clear();
        Console.WriteLine($"Quelle question voulez-vous supprimer (prenez un nombre de 1 à {quests.Count}, et 0 pour annuler)?");
        String? input = Console.ReadLine();
        int quesSuppr = 0;
        if (!Int32.TryParse(input, out quesSuppr)) quesSuppr = 0;

        if (quesSuppr >= 1 || quesSuppr <= quests.Count)
        {
            quests.RemoveAt(quesSuppr - 1);
            Console.WriteLine($"Question {quesSuppr} supprimée !");
        }
        else if (quesSuppr != 1)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Vous deviez entrer un nombre enre 1 et {quests.Count} !");
            Console.ResetColor();
        }
        else Console.WriteLine("Decision annulée.");

        Console.WriteLine($"Appuyez sur une touche...");
        Console.ReadKey();
        Console.Clear();
    }

    static void showStatistiques()
    {
        int nbrePassages = Candidat.NombrePassages.Sum();
        Console.Clear();
        Console.WriteLine($"Nous avons eu {nbrePassages} passages.");
        string header = String.Format("{0,-10}|{1,-10}|{2,-10}", "Notes", "Effectif", "Pourcentage (%)");
        Console.WriteLine(header);
        for (int i = 0; i < (int)NiveauCandidat.NBRE_NIVEAUX; i++)
            Console.WriteLine(String.Format("{0,-10}|{1,10}|{2,10:P1}", Passage.performanceSpan[i], Candidat.NombrePassages[i], (nbrePassages == 0) ? 0 : ((double)Candidat.NombrePassages[i] / nbrePassages)));
    }

    static void passerTest(Candidat c)
    {
        int reponse, bonnesReponses = 0;
        int NBRE_QUESTIONS = quests.Count;
        Console.WriteLine($"Vous devrez répondre à nos {NBRE_QUESTIONS} question(s).");
        for (int i = 0; i < NBRE_QUESTIONS; i++)
        {
            Console.WriteLine($"Question {i + 1} - {quests[i].enonce}");

            for (int j = 0; j < 4; j++) Console.WriteLine($"\t\t{j + 1} - {quests[i].reponses[j]}");

            String? input = Console.ReadLine();
            // N'ayant pas entré un nombre, le candidat doit par conséquent avoir donné une mauvaise réponse
            if (!Int32.TryParse(input, out reponse)) reponse = -1;

            quests[i].Repondre(reponse);
            if (quests[i].bonneReponse) bonnesReponses++;
            Console.WriteLine($"");
        }

        Console.Write($"Vos résultats finaux sont de ");
        Passage pass = new Passage(bonnesReponses, NBRE_QUESTIONS);
        NiveauCandidat niv = Passage.ClasserCandidat(bonnesReponses, NBRE_QUESTIONS);
        Candidat.NombrePassages[(int)niv]++;
        Console.ForegroundColor = Passage.performanceCandidatCouleur[(int)niv];
        Console.WriteLine($"{bonnesReponses}/{NBRE_QUESTIONS} ({100 * (double)bonnesReponses / NBRE_QUESTIONS} %)");
        Console.ResetColor();
        Console.WriteLine($"Votre performance a été {Passage.performanceCandidat[(int)niv]}");
        Console.WriteLine($"Revue des réponses");

        // Correction
        for (int i = 0; i < quests.Count; i++)
        {
            Console.ForegroundColor = (quests[i].bonneReponse) ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"Question {i + 1} - {quests[i].enonce}");

            for (int j = 0; j < 4; j++) Console.WriteLine($"\t\t{j + 1} - {quests[i].reponses[j]}");

            Console.WriteLine($"{(quests[i].bonneReponse ? "REPONSE CORRECTE" : "MAUVAISE REPONSE")}");
            Console.ResetColor();
            if (!quests[i].bonneReponse) Console.WriteLine($"La bonne réponse était {quests[i].repCorrecte}");
        }
    }

    static void gererCompte(Candidat c)
    {
        Console.Clear();
        bool exit = false;
        do
        {
            Console.WriteLine("Que voule-vous faire ?\n(1) Voir vos informations personnelles\n(2) Voir vos résutats\n\t((0) Quitter)");
            int choix = -1;
            string? input="";

            do
            {
                input = Console.ReadLine();
            } while (!Int32.TryParse(input, out choix));
            
            switch (choix)
            {
                case 0:
                    Console.WriteLine("Au revoir !");
                    exit = true;
                    break;
                case 1:
                    Console.Clear();
                    Console.WriteLine($"Nom : {c.Nom}\nPrénom : {c.Prenom}\nIdentifiant : {c.ID}\n\n");
                    bool sortie = false;
                    do {
                        Console.WriteLine("Voulez-vous modifier votre nom / prénom (O / N) ? ");
  
                          input = Console.ReadLine().ToLower();

                        if(input=="o"|| input == "oui")
                        {
                            Console.WriteLine("Entrez votre nouveau nom (laisez vide pour ne rien changer) :");
                            input = Console.ReadLine();

                            if (input != "") c.Nom = input;

                            Console.WriteLine("Entrez votre nouveau prénom (laisez vide pour ne rien changer) :");
                            input = Console.ReadLine();
                            if (input != "") c.Prenom = input;

                            sortie = true;
                        }
                        else if (input == "n" || input == "non")
                        {
                            sortie = true;
                        }
                    } while (!sortie);
                    Console.WriteLine("Appuyez sur une touche...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                case 2:
                    Console.Clear();
                    int nbrePassages = c.ListePassages.Count();
                    int[] tabPerformanceIndividuelles = new int[(int)NiveauCandidat.NBRE_NIVEAUX];
                    foreach(Passage p in c.ListePassages)
                    {
                        tabPerformanceIndividuelles[(int)p.Niveau]++;
                    }
                    Console.Clear();
                    Console.WriteLine($"Vous avez passé {nbrePassages} examen(s).");
                    string header = String.Format("{0,-10}|{1,-10}|{2,-10}", "Notes", "Effectif", "Pourcentage (%)");
                    Console.WriteLine(header);
                    for (int i = 0; i < (int)NiveauCandidat.NBRE_NIVEAUX; i++)
                        Console.WriteLine(String.Format("{0,-10}|{1,10}|{2,10:P1}", Passage.performanceSpan[i], tabPerformanceIndividuelles[i], (nbrePassages == 0) ? 0 : ((double)tabPerformanceIndividuelles[i] / nbrePassages)));

                    Console.WriteLine("Appuyez sur une touche...");
                    Console.ReadKey();
                    Console.Clear();
                    break;
                default:
                    break;
            }
        } while (!exit);
    }

    // Pour le candidat
    static void CandidatQCM() {
        Console.Clear();
        Console.WriteLine("Bienvenue à notre test de recrutement.");
        Console.WriteLine($"Etes-vous un nouveau candidat (O/N)?");
        string? input = Console.ReadLine();
        uint IDCandidat = 0; // Les ID des utilisateurs ne commençant qu'à 1, c'est la garantie qu'il ne prendra pas de compte valide, sachant que le programme EXIGE une variable initialisée
        bool quitterFonction = false, connectionUser=false;
        Candidat cand;

        // Connexion de l'utilisateur
        do {
            if (input.ToLower() == "oui" || input.ToLower() == "o")
            {
                Console.WriteLine($"Entrez votre nom :");
                cand = new Candidat();
                input = Console.ReadLine();
                cand.Nom = input;
                Console.WriteLine($"Entrez votre prénom :");
                input = Console.ReadLine();
                cand.Prenom = input;
                Console.WriteLine($"Votre ID est : {cand.ID}");
                ListeCandidats.Add(cand.ID, cand);
                IDCandidat = cand.ID;
                connectionUser = true;
            }
            else if (input.ToLower() == "non" || input.ToLower() == "n")
            {
                bool DemanderNombre = false;

                // Demander l'ID
                do {
                    Console.WriteLine($"Entrez votre numéro d'identification :");
                    DemanderNombre = UInt32.TryParse(Console.ReadLine(), out IDCandidat);

                    if (!DemanderNombre)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine($"Vous deviez entrer un nombre !");
                        Console.ResetColor();
                    }
                } while (!DemanderNombre);

                // Cherche s'il est présent
                if (ListeCandidats.ContainsKey(IDCandidat))
                {
                    Console.WriteLine($"Candidat {IDCandidat} présent.");
                    cand = ListeCandidats[IDCandidat];
                    connectionUser = true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine($"Le candidat n°{IDCandidat} n'existe pas !");
                    Console.ResetColor();
                    quitterFonction = true;
                    connectionUser = true;
                }
            }
        } while (!connectionUser);

        // Utilisation du compte (tests et accès aux infos)
        if (!quitterFonction)
        {
            // Message de bienvenue
            Console.WriteLine($"Bienvenue, {ListeCandidats[IDCandidat].Prenom} {ListeCandidats[IDCandidat].Nom.ToUpper()} ({ListeCandidats[IDCandidat].ID}) !");
            bool keep = false;

            do
            {
                Console.WriteLine($"Que voulez-vous faire ?\n(1) Passer le test\n(2) Gérer le compte\n\t((0) pour quitter)");
                input = Console.ReadLine();
                int choix;
                if(!Int32.TryParse(input,out choix))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Error.WriteLine($"Vous deviez entrer un nombre !");
                    Console.ResetColor();
                    continue;
                }
                else
                {
                    switch (choix)
                    {
                        case 1:
                            passerTest(ListeCandidats[IDCandidat]);
                            break;
                        case 2:
                            gererCompte(ListeCandidats[IDCandidat]);
                            break;
                        case 0:
                            Console.WriteLine("Au revoir !");
                            keep = true;
                            break;
                        default:
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Error.WriteLine("Vous deviez entrer un nombre valide !");
                            Console.ResetColor();
                            break;
                    }
                }
                
            } while (!keep);
        }

        Console.WriteLine("Appuyez sur une touche...");
        Console.ReadKey();
        Console.Clear();
    }

    // Gérer QCM
    static void AdminQCM()
    {
        Console.Clear();
        Console.WriteLine("Entrez votre mot de passe :");
        String? input = Console.ReadLine();
        int choix = -1;

        if (input == "Admin2022")
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\t\tBienvenue, cher admin !");
            Console.ResetColor();
            do
            {
                Console.WriteLine("Que voulez-vous faire ? :\n\t(1) Liste des questions\n\t(2) Ajouter une question\n\t(3) Modifier une question\n\t(4) Supprimer une question\n\t(5) Statistiques des candidats\n\t(6) Liste des candidats\n\t(7) Modifier un candidat\n\t(8) Supprimer un candidat\n\n((0) Quitter)");
                input = Console.ReadLine();
                if (!Int32.TryParse(input, out choix)) choix = -1;

                switch (choix)
                {
                    case 0:
                        Console.WriteLine("Au revoir, cher admin.");
                        break;
                    case 1:
                        listerQuestions();
                        break;
                    case 2:
                        ajouterQuestions();
                        break;
                    case 3:
                        modifierQuestion();
                        break;
                    case 4:
                        supprimerQuestion();
                        break;
                    case 5:
                        showStatistiques();
                        break;
                    case 6:
                        listerCandidats();
                        break;
                    case 7:
                        Console.WriteLine("Fonctionnalité pas encore implémentée");
                        break;
                    case 8:
                        Console.WriteLine("Fonctionnalité pas encore implémentée");
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Error.WriteLine("\t\tVous devez entrer un nombre !");
                        Console.ResetColor();
                        break;
                }
                Console.WriteLine("########");
                Console.WriteLine("Appuyez sur une touche...");
            } while (choix != 0);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine("\t\tMAUVAIS MOT DE PASSE !!!");
            Console.ResetColor();
            Console.WriteLine("Appuyez sur une touche...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    static void listerCandidats()
    {
        Console.Clear();
        if (ListeCandidats.Any())
        {
            Console.WriteLine("Voici la liste des étudiants (ID, nom complet et nombre passages) :");
            foreach (KeyValuePair<uint, Candidat> cand in ListeCandidats)
            {
                Console.WriteLine($"{cand.Value.ID} - {cand.Value.Prenom} {cand.Value.Nom} - {cand.Value.ListePassages.Count}");
            }
        }
        else
        {
            Console.WriteLine("Aucun candidat enregistré.");
        }
        Console.WriteLine("Appuyez sur une touche...");
        Console.ReadKey();
        Console.Clear();
    }
}