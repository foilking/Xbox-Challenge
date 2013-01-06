Xbox-Challenge
==============

This web site was originally done as a code challenge for a job application. The company gave a webservice, handling storing the game and voting data. I had done the original project in 10 hours and have spend about 3 hours pulling out the service reference and creating a semi-standard data storage layer.

Without giving too much away, here are some of the specifications that were given for the project:

## Description
ACME Software needs an application to track the interest in games for our Nintendo Wii. This web site will display the games we own, the games we would like to buy, and the number of votes for each game. Employees should be able to vote for their favorite game or add a new game to this list once per day. At the end of the week, if we bill enough hours, the game with the most votes will be purchased and marked as a game we currently own. The site   

## Requirement
* Display list of games that are wanted and owned
    * Separate games by wanted or owned
* Ability to vote for a game
* Ability to add a new game
* Allow only one vote per peson per weekday
    * Because we're expecting employees to only vote from their work laptops, use cookies to store when a person has voted
    * Give a message when an employee has already voted or when it's the weekend
* Ability to mark a game as owned
