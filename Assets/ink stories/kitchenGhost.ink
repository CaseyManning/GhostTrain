VAR title = "GORDON"
VAR replayable = true
VAR hasfryingpan = false
VAR cooked = false

- Oh hi there!

* [Is this your kitchen?]

- It is! Or was, maybe. Not too many people need me to cook anything for them anymore. 

* [Oh no!] -> startt
* [Can ghosts eat food?]

- I'm not sure, really. Haven't gotten around to trying it.

* [Would you like to?] -> startt

===startt===
You know, I have been craving some of those special pancakes I used to make.

Do you think you could help me make them?

* [Sure]

Oh good! While I start working on the batter, do you think you could find us a good pan to use?
->END

==replay==
{cooked: Thank you for helping me cook again! -> END}

{not cooked: Have you found my frying pan?}

* { hasfryingpan } [Right here!] -> foundpan
* { not hasfryingpan } [Not yet]

- Trying looking around on the counters, I must have left it around here somewhere. -> END

===foundpan===
Excellent, now lets get cooking! #remove fryingpan

* [How can I help?]

- My special pancakes need to be flipped five times before they are ready. Just take the pan and keep flipping until it looks done!

My pancakes have a tendency to go flying, so make sure you catch them on the way down! #startcooking 
~cooked = true
-> END