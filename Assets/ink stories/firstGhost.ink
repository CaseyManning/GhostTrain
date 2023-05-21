VAR title="GHOST CHILD"
VAR replayable = true
VAR hasnoArmTeddyBear = false
VAR hasrightArmOfTeddyBear = false
VAR hasfullTeddyBear = false
VAR completedQuest = false

You hear a low, murmuring sound as you approach.

* [Hello!] 
    Oh hi! How are you?
    ** [Pretty good!]
        Oh. Well I guess that's good to hear.
    ** [I'm not too sure. I think I just woke up?]
        I guess I just woke up as well, depending on how you think about it.
* [Wow! A real ghost!]
    Oh. Are ghosts surprising?
    ** [Of course!]
        I guess I was also a bit surprised to be a ghost. 
        
        I did watch you just put on a pair of ghost glasses though, so you could've been expecting it a little bit.
    ** [I guess not really]
        Well that's good. I wouldn't want to be going around surprising people forever.
        
- Have you seen a teddy bear anywhere around here? It was sitting on the table next to me just recently, and it can't have gone far.

* [No I haven't.]

* [Yeah, I've seen one.]
    That's so good to hear. Where?
        ** [Well,]
            Hm?
            *** [Okay I guess I didn't actually see one. I lied.]
                Why would you do that? This teddy bear is important to me, you know.
                    **** [I'm not sure]
       
- Oh. Okay.
* [I could help you find it though!]

You could? Well, that would be really nice of you.

I'll just be here waiting.
->END

===replay===

- {completedQuest: Thank you for finding my teddy bear! -> END}

- {not completedQuest: Have you found my teddy bear yet?}

* { not hasnoArmTeddyBear }{ not hasfullTeddyBear} [No I'm still looking] -> END

* { hasnoArmTeddyBear } { not hasrightArmOfTeddyBear}[I found this!] -> noarm

* {hasrightArmOfTeddyBear} {hasnoArmTeddyBear}[ I've got the pieces] -> pieces

* { hasfullTeddyBear } [Here it is!] ->foundit

===noarm===
Hey that looks like it! But it's missing one of it's arms...

It's not a teddy bear without both arms. Try looking around some more. -> END
===foundit===
Wow!

You found my teddy bear!! 
~completedQuest = true
#remove fullTeddyBear
Thank you very much. -> END

===pieces===
Well those are the pieces of my teddy bear, but I was sort of hoping for a whole bear.

Do you think you could put it together for me?
->END
