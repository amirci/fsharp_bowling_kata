namespace BowlingKata

[<AutoOpen>]
module BowlingGame =

    type Frame = int * int
    type Game  = Frame list

    let Score aGame = 
        let (|Strike|Spare|Lame|) (a, b) = 
            match a + b with
            | 10 when a = 10 -> Strike
            | 10 -> Spare
            | _  -> Lame

        let rec calculate throws (score:int) frames =
            let next = calculate (throws - 1)
            match throws, frames with
            | 0, _  -> score
            | _, [] -> score
            | _, Spare::(a, b)::rest -> 
                next (score + 10 + a) ((a, b)::rest)
            | _, Strike::Strike::(a, b)::rest ->
                next (score + 20 + a) ((10, 0)::(a, b)::rest)
            | _, Strike::(a, b)::rest ->
                next (score + 10 + a + b) ((a, b)::rest)
            | _, (a, b)::rest ->
                next (score + a + b) rest

        aGame |> calculate 10 0