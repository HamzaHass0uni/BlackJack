using System;
using System.Collections.Generic;

namespace BlackJack
{

	public class BlackJack
	{

		string[] colors = {"carreau","trefle","pique","coeur"};
		string[] values = {"1","2","3","4","5","6","7","8","9","10","V","D","R"};
		List<string> jeu = new List<string>();
		List<string> cartesBanque = new List<string>();
		List<string> cartesJoueur = new List<string>();
		Random rand = new Random();
		
		public void ConstruireJeu(){
			foreach(string val in values){
				foreach(string col in colors){
					jeu.Add(val + " " + col);
				}
			}
//			string carte = TirerCarte();
//			string carte2 = TirerCarte();
			cartesBanque.Add(TirerCarte());
			cartesBanque.Add(TirerCarte());
			Console.WriteLine("Première carte banque : " + cartesBanque[0]);
			JeuJoueur();
		}
		public void JeuJoueur(){
			cartesJoueur.Add(TirerCarte());
			cartesJoueur.Add(TirerCarte());
			AfficherJeu(cartesJoueur,"joueur");
			DemandeTirerCarte();
		}
		public void DemandeTirerCarte(){
			Console.WriteLine("Voulez-vous encore une carte (o/n) ?");
			string choix = Console.ReadLine();
			if(choix=="o" && CalculerCartes(cartesJoueur) < 21){
				cartesJoueur.Add(TirerCarte());
				AfficherJeu(cartesJoueur,"joueur");
				DemandeTirerCarte();
			}else{
				AfficherJeu(cartesBanque,"banque");
				JeuBanque();
			}
		}
		public int CalculerCartes(List<string> cartes){
			int total = 0;
			foreach(string carte in cartes){
				string[] chunks = carte.Split(new char[]{' '});
				if(chunks[0] == "V" || chunks[0] == "D" || chunks[0] == "R"){
					total = total + 10;
				}else{
					int val = Int32.Parse(chunks[0]);
					total = total + val;
				}
			}
			return total;
		}
		public void Terminer(){
			int sommeJoueur = CalculerCartes(cartesJoueur);
			int sommeBanque = CalculerCartes(cartesBanque);
			if(sommeJoueur > 21){
				Console.WriteLine("Banque gagne, joueur perd.");
			}else if(sommeBanque > 21){
				Console.WriteLine("Joueur gagne, banque perd.");
			}else if(sommeJoueur > sommeBanque){
				Console.WriteLine("Joueur gagne, banque perd.");
			}else if(sommeJoueur == sommeBanque){
				Console.WriteLine("Egalité !!! Récupérez votre mise.");
				}else{
				Console.WriteLine("Banque gagne, joueur perd.");
			}
		}
		public void JeuBanque(){
			if(CalculerCartes(cartesBanque) < 17){
				Console.WriteLine("Banque tire une nouvelle carte...");
				cartesBanque.Add(TirerCarte());
				AfficherJeu(cartesBanque,"banque");
				JeuBanque();
			}else{
				Terminer();
			}
			
		}
//		public void AfficherJeuBanque(){
//			foreach(string carte in cartesBanque){
//				Console.WriteLine("carte banque: " + carte);
//			}
//			Console.WriteLine("Total des points banque : " + CalculerCartes(cartesBanque));
//		}
//		public void AfficherJeuJoueur(){
//			foreach(string carte in cartesJoueur){
//				Console.WriteLine("carte joueur: " + carte);
//			}
//			Console.WriteLine("Total des points joueur : " + CalculerCartes(cartesJoueur));
//			for(int i = 0; i < cartesJoueur.Count;i++){
//				Console.WriteLine("carte joueur: " + cartesJoueur[i]);
//			}
//		}
		public void AfficherJeu(List<string> jeu, string origine){
//			string origin = "banque";
//			if(origine > 0){
//				origin = "joueur";
//			}
			foreach(string carte in jeu){
				Console.WriteLine("carte "+origine+": " + carte);
			}
			Console.WriteLine("Total des points "+origine+": " + CalculerCartes(jeu));
		}
		public string TirerCarte(){
			int aRetirer = rand.Next(jeu.Count);
			string carte = jeu[aRetirer];
			jeu.RemoveAt(aRetirer);
			return carte;
		}
	}
}
