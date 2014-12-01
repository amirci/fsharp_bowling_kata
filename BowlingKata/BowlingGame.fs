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

        let nextTwoRolls = function
        | Strike::(a, b)::tail -> 10, a
        | frame ::tail         -> frame
        | []                   -> 0, 0

        let rec calculate score throw frames =
            match throw, frames with
            | 0, _  -> score
            | _, [] -> score
            | _, (a, b)::rest ->
                let r1, r2 = rest |> nextTwoRolls
                let bonus = match (a, b) with 
                            | Strike -> r1 + r2
                            | Spare  -> r1
                            | _      -> 0

                calculate (score + a + b + bonus) (throw - 1) rest

        aGame |> calculate 0 10