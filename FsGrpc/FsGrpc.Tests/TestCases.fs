module TestCases
open Helpers
open Test.Name.Space


let Value1 =
    """
    0807119a99999999d95e401d3930000022086368c3a9766572652a0dc2af5c5f
    28e38384295f2fc2af3500806a4338ce958f014095fed2014915bf3400000000
    00500158c803653702000069cbeb0b040000000070a31378d124
    """ |> sanitizeHex,
    { TestMessage.empty with
        TestInt = 7;
        TestDouble = 123.4;
        TestFixed32 = 12345u;
        TestString = "chévere"
        TestBytes = Google.Protobuf.ByteString.CopyFromUtf8("""¯\_(ツ)_/¯""")
        TestFloat = 234.5f
        TestInt64 = 2345678L
        TestUint64 = 3456789UL
        TestFixed64 = 3456789UL
        TestBool = true
        TestUint32 = 456u
        TestSfixed32 = 567
        TestSfixed64 = 67890123L
        TestSint32 = -1234
        TestSint64 = -2345 }

let Value2 = { TestMessage.empty with TestString = "🤔" }

let Value3 =
    { Nest.empty with
        Name = "Animal"
        Children = [|
            { Nest.empty with
                Name = "Mammal" }
            { Nest.empty with
                Name = "Fish"
                Special = Some Special.empty }
        |]
        Inner = Some { Nest.Inner.empty with InnerName = "inner" }
        }

let Value4 =
    { Special.empty with
        IntList = [|1; 2|]
        DoubleList = [|2.0; 3.0|]
        StringList = [|"One"; ""; "Three"|]
        Dictionary = Map [("One", "Uno"); ("Two", "Dos")]
        }

open Enums
let Value5 =
    { Enums.empty with
        MainColor = Color.Red
        OtherColors = [|Color.Black; Color.Red; Color.Blue|]
        ByName = Map [("red", Color.Red); ("blue", Color.Blue)]
        Union = UnionCase.Name "green"
        }

let Value6 =
    { Google.empty with
        Int32Val = Some 0 
        StringVal = Some "X"
        Timestamp = Some (NodaTime.Instant.FromUnixTimeMilliseconds 12345678L)
        Duration = Some (NodaTime.Duration.FromMilliseconds 12345678L)
        }