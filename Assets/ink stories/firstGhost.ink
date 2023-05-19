VAR title="GHOST CHILD"
VAR replayable = true
VAR hasnoArmTeddyBear = false
VAR hasrightArmOfTeddyBear = false
VAR hasfullTeddyBear = false
VAR completedQuest = true
[You have encountered a ghost child and you start a conversation with the ghost child.]

*Do you like my new goggles? 
*Hey, you seem sad… -> Ghost_Child_2

- Yes, I love them! They look spooky.

*That’s because they let me see ghosts!
*You wouldn’t happen to be a ghost, would you? -> 1_1

- Coooool! That must come in handy around here! 

*You wouldn’t happen to be a ghost, would you? -> 1_1

===1_1===

I am a ghost! 

*Is that why you’re sad? -> Ghost_Child_2
*What’s it like being a ghost? 

- I don’t know. What’s it like being anything? I’m kinda sad.

*Why are you sad? -> Ghost_Child_2

===Ghost_Child_2===

I’m just sad because I lost my toy… 
* Oooh! With my new kit I can help you find it! 


- Really? You would do that for me? 

* Of course! 

- Awesome! It is a teddy bear -> Ghost_Child_Task


===Ghost_Child_Task===

Make sure to check everywhere in this carriage, I miss my toy :(

*I will help you!

- Navigate to the next train car and look for the missing toy -> DONE

===replay===
{not completedQuest: Have you found my teddy bear yet?}

{completedQuest: Thank you for finding my teddy bear! -> END}

* { not hasnoArmTeddyBear }{ not hasfullTeddyBear} No I'm still looking -> END

* { hasnoArmTeddyBear } { not hasrightArmOfTeddyBear} I found this! -> noarm

* {hasrightArmOfTeddyBear} {hasnoArmTeddyBear} I've got the pieces -> pieces

* { hasfullTeddyBear } Here it is! ->foundit

===noarm===
- Hey that looks like it! But wait a minute, its missing one of it's arms...

- It's not a teddy bear without both arms! Try looking around some more. -> END
===foundit===
- Wow!

you found my teddy bear!! 
~completedQuest = true
#remove fullTeddyBear
Thanks so much :D -> END

===pieces===
My bear is in pieces! That's no good, what am I supposed to do with two pieces of a teddy bear?

Do you think you could put it together for me?
->END
