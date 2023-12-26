// the ring definitions, which we're never going to modify
let firstRing: int list = [ 0; 16; 8; 4; 15; 7; 10; 1; 10; 4; 5; 3; 15; 16; 4; 7 ]

let outerRings: int list list =
    [ [ 6; 10; 8; 10; 9; 8; 8; 9 ]
      [ 0; 11; 8; 8; 8; 10; 11; 10 ]
      [ 3; 8; 10; 14; 11; 8; 12; 11 ]
      [ 6; 6; 8; 8; 16; 19; 8; 17 ] ]

let innerRings: int list list =
    [ [ 13; 11; 13; 10; 18; 10; 10; 10; 10; 15; 7; 19; 18; 2; 9; 27 ]
      [ 5; 1; 24; 8; 10; 20; 7; 20; 12; 1; 10; 12; 22; 0; 5; 8 ]
      [ 20; 8; 19; 10; 15; 29; 12; 20; 13; 13; 0; 22; 19; 10; 0; 5 ]
      [ 10; 17; 10; 5; 6; 18; 8; 17; 4; 20; 4; 14; 4; 5; 1; 14 ] ]

// a couple of arrays, which we DO want to modify, `shown` will contain our answer when we're done
let mutable rotations: int array = Array.create 4 0
let mutable shown: int array array = Array.init 4 (fun _ -> Array.zeroCreate 16)

let rec solve (ring: int, sums: int list, priorRotation: int) =
    if ring = 4 then
        // we're either done or we're not
        sums |> List.forall (fun s -> s = 50)
    else
        [| 0..15 |]
        |> Array.exists (fun rotation ->
            let shownNumber slot =
                // figure out which two half-subsets of the available numbers are shown
                if (rotation &&& 1) = (slot &&& 1) then
                    outerRings.[ring].[((slot + 16 - rotation) &&& 15) / 2]
                else
                    innerRings.[ring].[((slot + 16 - priorRotation) &&& 15)]

            Array.set rotations ring rotation
            // get the 16 visible numbers
            let newShown = [| for s in 0..15 -> shownNumber (s) |]
            // and replace the shown sublist with the new set of numbers
            Array.set shown ring newShown
            let nextSums = [ for s in 0..15 -> sums.[s] + shown.[ring].[s] ]

            if List.max (nextSums) > 50 then
                false // prune the branch and stop recursing
            else if solve (ring + 1, nextSums, rotation) then
                printfn "%A" shown
                System.Environment.Exit(0)
                // I'm sure there's some more idiomatic way to do this
                // this feels...weird, since it is unreachable, but that's what the compiler wants
                true
            else
                false)

solve (0, firstRing, 0)
