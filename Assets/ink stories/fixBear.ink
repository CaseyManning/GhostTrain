#test1
VAR title = "SPOOL OF THREAD"
VAR hasnoArmTeddyBear = false
VAR hasrightArmOfTeddyBear = false
VAR replayable = true
- A spool of thread and a needle.
->replay
===replay===
- Perhaps I could use it?
    * {hasnoArmTeddyBear} {hasrightArmOfTeddyBear}[ Maybe I can use this to repair the teddy bear's arm!]->bear_fixed
    * {(not hasrightArmOfTeddyBear) || (not hasnoArmTeddyBear)} [Find the rest of the teddy bear first to use the spool of thread and needle!] -> DONE

===bear_fixed===
// * Use the spool of thread and needle to fix the teddy bear.
#remove rightArmOfTeddyBear
#remove noArmTeddyBear
#remove spool
#gain fullTeddyBear
You have fixed the bear and it looks brand new! -> DONE