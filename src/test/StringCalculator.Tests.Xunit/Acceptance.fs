module Tests.Acceptance

open Acceptance.Steps
open Xunit


let init() =
    res <- None
    message <- None    
    logList.Clear()
    
[<Fact>]
let ``I can sum two numbers`` () =
    init()
    ``Given I have entered`` "1,2" ``into the calculator``
    ``Then the result is`` 3


[<Fact>]
let ``I can parse an empty string`` () =
    init()
    ``Given I have entered`` "" ``into the calculator``
    ``Then the result is`` 0

[<Fact>]    
let ``I can sum more numbers``() =
    init()
    ``Given I have entered`` "12,22,90,11" ``into the calculator``
    ``Then the result is`` 135

[<Fact>] 
let ``I cannot sum negative numbers`` () =
    init()
    ``Given I have entered`` "-1,2,3,-4" ``into the calculator``
    ``Then I fail with`` "Cannot add this negative numbers: -1 -4"

[<Fact>]    
let ``Can change the delimiter``() =
    init()
    ``Given I have entered`` "//[;]\n1;2" ``into the calculator``
    ``Then the result is`` 3

[<Fact>]    
let ``Will ignore numbers greater than 1000``() =
    init()
    ``Given I have entered`` "2,3,5,1001,7" ``into the calculator``
    ``Then the result is`` 17

[<Fact>]    
let ``I can use delimiters of any length``() =
    init()
    ``Given I have entered`` "//[***]\n1***2***3" ``into the calculator``
    ``Then the result is`` 6
    
[<Fact>]    
let ``I can use an arbitrary number of delimiters of any length``() =
    init()
    ``Given I have entered`` "//[*][%]\n2*2%3" ``into the calculator``
    ``Then the result is`` 7

[<Fact>]    
let ``I can do it all``() =
    init()
    ``Given I have entered`` "//[**][%%%][$]\n2**2%%%3$4%%%1001$2000**3" ``into the calculator``
    ``Then the result is`` 14
