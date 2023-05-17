#test1
VAR replayable = true
VAR teddyBearFound = false
-Oh look! Maybe I can use this spool for something useful!
{ teddyBearFound:
    - You can use the spool to stitch the teddy bear! You feel a sense of relief. -> bear_found
    - You haven't found the teddy bear yet. You feel a sense of unease. -> bear_missing
}
== bear_found ==
With the teddy bear in your possession, you feel ready to face any challenges that come your way. -> DONE

== bear_missing ==
Without the teddy bear, you feel vulnerable and a little scared. You decide to continue your search. -> DONE

