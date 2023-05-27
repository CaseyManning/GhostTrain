VAR title = "BORT"
VAR hoped = false
VAR getting = false
VAR replayable = true

You approach a ghost repeatedly stabbing his transparent arm with a small fork. He seems to be focused quite intently on this activity.

* [Hello!]

- He seems to ignore you for a second, but then slowly looks up at you.

Oh hi.

* [What are you doing with that fork?]

Well you see, when I was alive, I'd always wanted to give my self a good stab in the arm with a fork. One of those nice sharp ones, just really dig it in deep. 

Of course, I never quite got up the courage to actually do it, much to the satisfaction of everyone around me.

After I died, it rather quickly ocurred to me that I could now give myself a quick stab quite easily. 
->uah
=== uah ===

* { not hoped } [Is it as good as you'd hoped?]

    Sadly, I'm not sure it is. I'd imagine a real arm would be a bit juicy, something you could really dig into and squish around. But at this point I'm a bit too permeable for anything like that. 
    ~hoped = true
    ->uah

* { not getting } [What are you getting out of this?]

    I'm not entirely sure. A sense of satisfaction in doing that which I've wanted to do for so long? The joy in cherishing the little sensations that we have left? Or maybe not all that much, really. Who knows.
    ~getting = true
    ->uah

* { hoped && getting } [Can I try stabbing you?] 
    What? Stab me? Are you some kind of sadist?
    
    Of course you can't stab me! -> END
* { hoped && getting} [Can ghosts actually feel anything?]

    Not like people can, given the whole "lacking a nervous system" situation. But yes, I can feel it. The cool presence of a small, metallic creature inside me, it's sharp edges flowing past my foggy insides. It's quite pleasant not having nerves, really.
    ->END

=== replay ===

Bort is still stabbing his arm with a fork, rotating it to enter from different angles.
->END
