VAR title = "EDITH"
VAR haslemonade = false
VAR asked = false
VAR replayable = true

 While ghost facial expressions are almost entirely uninterpretable, you feel as though you might be able to make out a slight frown.

* [Hello!]

* [Are you my mother?]

    What?

    **[Just thought I'd ask, you look a little bit like her]

    Well everyone looks just about the same when they're a ghost, don't they? It still feels to me like I look the same though, somehow.
    
    But no, to clear things up for you, I have a feeling that I'm probably not your mother.
    ~asked = true
    
    *** [Oh, okay.]
    ->replay

===replay===
- Can I help you with anything?

* {haslemonade} [Would you like some of my lemonade?]

    You know, that sounds delicious. I would love a taste.
    
    Edith takes the lemonade and pours a little into her mouth. It falls through her directly onto the floor.
    
    ** [...]
    
        ... ->END

* [do you know where my mother is?]

{asked: Oh, so now because I look like her we must be friends?|Oh, what a terrible thing to be separated.}

No, I don't think I have. You might try looking further down the train though, I know a few folks ended up over there.

->END