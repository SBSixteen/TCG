// See https://aka.ms/new-console-template for more information
using System.ComponentModel;
using TCG.Board;
using TCG.Card;
using TCG.Utility;
using TCG.Card.Ability;

Logger Logger = Logger.getInstance();

CardFactory CardFactory = new CardFactory();
/*ICard L = CardFactory.ReadPublishedCard("Forest.card");
Logger.Log(L.toString());

L = CardFactory.CreateCreatureCard("Sly Fox", new int[] { 0,0,0,0,1,0}, "A fox that is quite smart.", "Uncommon", "Fox",  "HR:TG Base Pack", 1, 1, new List<IAbility>());
Logger.Log(L.toString());

Player G = new Player("Nabeel");
Board.SetOpposingPlayer(G);
L.Play();
Logger.Log(G.toString());
*/
//Game Logic Here

CardFactory.PublishCard(CardFactory.CreateLandCard("Island", new int[] { 0, 1, 0, 0, 0, 0 }, "", "Common", "HR:TG Base Pack"));
CardFactory.PublishCard(CardFactory.CreateLandCard("Swamp", new int[] { 0, 0, 1, 0, 0, 0 }, "", "Common", "HR:TG Base Pack"));
CardFactory.PublishCard(CardFactory.CreateLandCard("Plains", new int[] { 1, 0, 0, 0, 0, 0 }, "", "Common", "HR:TG Base Pack"));
CardFactory.PublishCard(CardFactory.CreateLandCard("Mountain", new int[] { 0, 0, 0, 1, 0, 0 }, "", "Common", "HR:TG Base Pack"));
CardFactory.PublishCard(CardFactory.CreateLandCard("Forest", new int[] { 0, 0, 0, 0, 1, 0 }, "", "Common", "HR:TG Base Pack"));
CardFactory.PublishCard(CardFactory.CreateCreatureCard("Savannah Lion", new int[] { 1, 0, 0, 0, 0, 0 }, "A lion from the plains of Africa", "Uncommon", "Cat", "HR:TG Base Pack", 2, 1, null));
CardFactory.PublishCard(CardFactory.CreateCreatureCard("African Elephant", new int[] { 1,0,0,0,0,1}, "An elephant from the plains of Africa", "Uncommon", "Elephant", "HR:TG Base Pack", 1, 3, null));
CardFactory.PublishCard(CardFactory.CreateCreatureCard("Galapagos Turtle", new int[] { 0, 1, 0, 0, 1, 0 }, "A turtle from the reefs of Oceania", "Uncommon", "Turtle", "HR:TG Base Pack", 0, 3, null));
IAbility dmg = new DamageAbility(2);
CardFactory.PublishCard(CardFactory.CreateSpellCard("Hell's Fireball", new int[] { 0, 0, 0, 2, 0, 0}, "A fireball summonned from hell", "Rare", "Fireball", "HR:TG Base Pack", dmg));
CardFactory.PublishCard(CardFactory.CreateSpellCard("Vial of Life", new int[] { 2, 0, 0, 0, 2, 0 }, "Small vial from the Fountain of Youth", "Rare", "Healing", "HR:TG Base Pack", new HealAbility(3)));
/*var d = CardFactory.CreateDeck();
foreach (ICard card in d)
{
    Logger.Log(card.toString());
}*/

Board.Initialize();
while (true)
{
    Board.Execute();
    if (Board.GetBoardState() == null)
    {
        break;
    }
}

Logger.Reveal();

