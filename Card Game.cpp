#include <iostream>
#include <string>
#include <array>
#include <cstdlib>
#include <stdlib.h> 
#include <stdio.h>
#include <time.h>
#include <unistd.h>

using namespace std;


int draw(int);
int display(int, int);
int cardi(int, int);
int press();



int main()
{

	int deck [3];

	int edeck [3];

	string card [3];

	int score {0};

	int choice{ 0 };

	int result{ 0 };
	
	string answer {0};

    string userName;
    


	cout << "Hello Welcome " << "\n";
	
	press();
    
	cout << "What is your name? " << "\n";
	getline(cin, userName);

	cout << "Hi, " << userName << " The rules are the first players to get to three kills wins. " << "\n";

    press();
	cout << "You and your opponent will get three cards each. " << "\n";
	
	press();

	cout << "Each cards will have their own Health and Attack Damage. " << "\n";
		
	press();
		
	cout<<"Do you wish to continue? y/n\n";
	
	cin >> answer;
	
	if (answer=="n")
	return 0;

	    for (int i{ 1 }; i < 4; ++i)
		deck[i] = draw(i);

        for (int i{ 1 }; i < 4; ++i)
		edeck[i] = draw(i);


	while (score != 3 && score != -3) {
		for (int i{ 1 }; i < 4; ++i)
			display(deck[i], i);
		//Have the player chose a card
		cin >> choice;
		result = cardi(deck[choice], edeck[rand() % 3 + 1]);
		if (result == 1){
			score++;
			cout<< "The current score is "<< score<<"\n\n";
			sleep(2);         //make the program wait for 5 seconds
		
		}
		if (result == -1){
			score--;
			cout<< "The current score is "<< score<<"\n\n";
			sleep(2);        
		
		}
		if (result == 0){
			cout << "There's no fun in a clean fight!\n\n";
			cout<< "The current score is "<< score<<"\n\n";
			sleep(2);        
		
			
		}
	}
	//display the score
	cout<< "The current score is "<< score<<"\n";
	
	return 0;
}


int draw(int num) {
	//Give both the player and enemy 3 random cards of a pool of 6 cards

	//unsigned int currentTime{ time(0) };
	int card{ 0 };
	if (num == 1) {
	long int currentTime{time(0)};
	srand(currentTime);
	}
	if (num == 2){
	    	long int currentTime{time(0)};
	srand(currentTime / 2);
	}
	if (num == 3){
	    	long int currentTime{time(0)};
	srand(currentTime + 2);
	}
	card = rand() % 6 + 1;

	return card;
}


int display(int card,int setnum) {
	//Display the Player's current cards
	int choice{ 0 };
	switch (card) {
	case 1:
		cout << "Press " << setnum << "to deploy your Space Age Warrior with 500 HP and 1250 ATK. \n\n";
		 break; 
	case 2:
		cout << "Press " << setnum << "to deploy your Ambiguous Cyborg with 1300 HP and 800 ATK.\n\n";
		 break; 
	case 3:
		cout << "Press " << setnum << "to deploy your Confetti Mech with 300 HP and 2000 ATK.\n\n";
		 break; 
	case 4:
		cout << "Press " << setnum << "to deploy your Illegal Alien with 1000 HP and 1000 ATK.\n\n";
		 break; 
	case 5:
		cout << "Press " << setnum << "to deploy your Legal Alien with 2000 HP and 300 ATK.\n\n";
		 break; 
	case 6:
		cout << "Press " << setnum << "to deploy your Empty Space with 5 HP and 1 ATK.\n\n";
		 break; 

	}

}


int cardi(int valu, int enemyCard) {

	//INITIALIZE THE VARIABLES USED
	std::string name;

	int HP{ 0 };

	int ATK{ 0 };

	std::string name1;

	int HP1{ 0 };

	int ATK1{ 0 };


	//Determine which card the player is using
	switch (valu) {

	case 1:

		name = "SpaceAgeWarrior";

		HP = 500;

		ATK = 1250;

		break; //optional


	case 2:

		name = "AmbiguousCyborg";

		HP = 1300;

		ATK = 800;

		break; //optional

	case 3:

		name = "ConfettiMech";

		HP = 300;

		ATK = 2000;

		break; //optional


	case 4:

		name = "IllegalAlien";

		HP = 1000;

		ATK = 1000;

		break; //optional


	case 5:

		name = "LegalAlien";

		HP = 2000;

		ATK = 300;

		break; //optional


	case 6:

		name = "EmptySpace";

		HP = 5;

		ATK = 1;

		break; //optional

	}


	//Do the same for the enemy

	switch (enemyCard) {

	case 1:

		name1 = "SpaceAgeWarrior";

		HP1 = 500;

		ATK1 = 1250;

		break; //optional


	case 2:

		name1 = "AmbiguousCyborg";

		HP1 = 1300;

		ATK1 = 800;

		break; //optional

	case 3:

		name1 = "ConfettiMech";

		HP1 = 300;

		ATK1 = 2000;

		break; //optional


	case 4:

		name1 = "IllegalAlien";

		HP1 = 1000;

		ATK1 = 1000;

		break; //optional


	case 5:

		name1 = "LegalAlien";

		HP1 = 2000;

		ATK1 = 300;

		break; //optional


	case 6:

		name1 = "EmptySpace";

		HP1 = 5;

		ATK1 = 1;

		break; //optional

	}

	//Case in which the player 1 shots the enemy

	if (ATK >= HP1) {

		cout << "Your " << name << " completely crushes the enemy's " << name1 << " 1 shotting them and giving you 1 point! \n";

		return 1;

	}
	//The player does not leave the enemy with a lot of health

	if (ATK < HP1) {

		int remain1 = ATK - HP1;

		cout << "Your " << name << " pounds the enemy's " << name1 << " leaving them with" << remain1 << " health! \n";

	}
	//The enemy 1 shots the player

	if (ATK1 <= HP) {

		cout << "However, the enemy's " << name1 << " completely crushes your " << name << " 1 shotting them and giving the enemy 1 point! \n";

		return -1;

	}
	//No cards die this turn

	if (ATK1 > HP) {

		int remain = ATK1 - HP;

		cout << "The enemy counters with their " << name1 << " smashing your " << name << " leaving them with" << remain << " health! \n";

		return 0;

	}



}

//makes the player press the enter key to continue.
 int press (){
     cout<< "Press enter to continue.\n";
     if (getchar())
     return 0;
     
     
 }


















