VAR title = "BORT"
VAR hoped = false
VAR getting = false
VAR replayable = true

You approach a ghost swinging around a very shiny fork.

* [Hello!]

- He starts excitedly waving the fork!

Have you ever seen such a marvolous fork?

* [huh...what about a fork?]

* [it's the shiniest fork I've ever seen!]

- When I was alive, I was on my way to the 1977 bi-annual hotel symposium to debut my grand fork! My fork was going to be in hotels around thE WoRLd!!

->uah
=== uah ===

* { not hoped } [bi-annual? which session did you go to?]

    Oh! It was November 1977! I never travel in the summer.. it's too stuffy in these trains.  
    ~hoped = true
    ->uah

* { not getting } [what happened?]

    I'm not entirely sure. I haven't gotten around to finding out yet, I had to make sure my fork survived first!
    ~getting = true
    ->uah

* { hoped && getting } [do you know where my guardian is?] 
    
    I wouldn't know where my fork is if it wasn't attached to me!-> END
* { hoped && getting} [i love your fork]

    Thanks kid!
    ->END

=== replay ===

Bort is still excitedly waving their fork!
->END
