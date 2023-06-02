VAR title = "BARTENDER"
VAR beatScore = false
VAR replayable = true

-Hey Kid! Not much I can offer you but how ‘bout a lemonade? 

*[I just woke up so I am pretty thirsty!]
*[I’m alright, I’m just looking for my parents right now. Have you seen any humans recently?] -> Bartender_Parents_Question

-Well here ya go! So tell me kid, what’s gotcha looking so blue? 

*[I’m just looking for my parents right now. Have you seen any humans recently?]->Bartender_Parents_Question

==Bartender_Parents_Question==
Wow, living humans? Here? Haven’t seen any since yesterday. 

But I have been playing a lot of darts. Mighta been distracted. 

*[Ooh, can I play? I could use a break from the search!]

-Of course, here are a set a darts.

*[Is there a score to beat?]

-I believe my high score is a 100, let's see if you can beat it!

*[Woo my first time trying but I will give it a shot!]

-If you think so! 
#startthrowing
-> END


==replay==
{beatScore: You are a rockstar! Wanna keep playing?}
*Yes, I'd love to! #startthrowing 
->END
*No thanks, I really have to find my parents. Thanks for the distraction though-> END
{not beatScore: Well, you are just a beginner. You can try again!}
*Where can I find more darts?
- I have plenty for both of us! 
*Perfect. I will give it a another try!
 #startthrowing
->END



