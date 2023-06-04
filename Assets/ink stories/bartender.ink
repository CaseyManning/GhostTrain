VAR title = "BARTENDER"
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

-If you think so! #startthrowing
-> END


==replay==
Hey! You are back again! Wanna play darts again?

*[Yes pls, I think I can do better this time!] #startthrowing 
->END
*[No thanks, I came to ask about my parents]

-Hmm, I still haven't seen your parents, come back to play darts anytime!
->END



