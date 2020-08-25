# Heracles
Technical assessment for Pleo

## The Challenge

_Given an amount of money as a number, format it as a string. Add associated tests for the functionality and for the user interface._

```js
formatMoney(2310000.159897); // '2 310 000.16'
formatMoney(1600); // '1 600.00'
```

_This needs to be a "fully working application" (you choose the format: web, cli, backend-frontend, mobile app, ...)_

_eg: A simple HTML page with an input box_

## The Thought Process

### Programming Language and Framework
I have used many programming languages in the past (Java, JS, Ruby, Python... Even Erlang), but once I became a team manager the amount
of time I could dedicate to code got reduced drastically. In my last company I started coding much more, this time in C#. I decided to 
be pragmatic and code the application in it, to be able to use the limitted time available to show how I design, code and 
test, rather than refreshing programming languages I've not used in several years.

In order to make the solution runable in any OS, and not to limit potential improvements later, I decided to use .NET Core 3.1.X. 

### Delivery
I chose to try GitHub Actions for CI. I had not used it before but I'm quite familiar with CI tools so it made sense to use the one
provided with GitHub, which was the repository hosting service requested by Pleo. 

As I could not know how much time I was going to be able to put on the exercise, I chose to follow an iterative approach to how the 
application would work. The idea is to increase the complexity of the architecture in each iteration, based on the time I have. The
iterations are as follow:
1. Console application, delivered as a project that can be built for Linux, Windows or Mac (through dotnet build)
2. Webservice, providing an external API that has the same functionality as the Console application. Provided as a docker container.
3. Web Application, a simple HTML frontend that uses the Webservice created in iteration 2. Solution provided as docker-compose containers.

### Methodology
As the specification is given by example, I decided to implement it using TDD. The steps are as follow:
1. Define the User-level AC
2. Break down the AC into UT cases
3. Implement a UT case
4. See the UT case fail
5. Implement the functionality to make the test pass
6. See the test pass
7. Iterate from 3
8. Refactor

#### User-level AC

The inferred generic User-level AC is as follow:

```
Given the user is prompted with entering an amount
When they input the desired money amount to be formatted
Then the money amount is converted to the Heracles format
```

The Examples provided allow to infer the actual format:

```
#UAC1
Given the user is promtped with entering an amount
When they input 2310000.159897
Then the Heracles formatter responds with '2 310 000.16'

#UAC2
Given the user is promtped with entering an amount
When they input 1600
Then the Heracles formatter responds with '1 600.00'
```

However, as we are talking about a money input, there are several AC that are implicit:

```
#UAC3
Given the user is promtped with entering an amount
When they input -1000.00
Then the Heracles formatter responds with error "You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number in digits"

#UAC4
Given the user is promtped with entering an amount
When they input $1000
Then the Heracles formatter responds with error "You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number in digits"

#UAC5
Given the user is promtped with entering an amount
When they input an empty amount
Then the Heracles formatter responds with error "You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number in digits"
```

There are multiple considerations around the behavior of the application that cannot be inferred from the Examples provided or the 
description of the exercise. Some examples are:

- How should Heracles ask for the amount? Which text? Which format?
- How does the application behave after providing the format transformation?
  - Does it terminate after showing the response? 
  - Does it restart immediatedly instead?\
  - Perhaps it asks the user what to do? How?

In order to 

#### 2. Breaking AC into Test Cases

Let's start with UAC1. From the format conversion we can infer the following:

```
TC1: heracles must prompt the user to enter a money amount and capture it
TC2: given an amount input, heracles should display it as a string with a given format
TC2.1: the resulting amount string should have 2 decimal points
TC2.2: the resulting amount string decimal points should be marked with a '.' symbol
TC2.3: if the input has more than 2 decimal points, the resulting amount string should show the decimals rounded up to the closest 2 decimal points
TC2.4: the resulting amount string should have an empty space as the thousand and million markers
TC2.5: the resulting amount string should be surrounded by the ' symbol
```

From UAC2 we can add the following:

```
TC3: if the input has no decimal points, the resulting amount string should add '.00' at the end
```

From UAC3-5 we can add:

```
TC4: if the input is a negative number, heracles should show an error with text 'You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number in digits'
TC5: if the input contains a non-digit character, heracles should show an error with text 'You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number in digits'
TC6: if the input is empty, heracles should show an error with text 'You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number in digits'
```


#### 3. Implement a Unit Test
These TCs will be implemented as Unit Test, shaping the functionality and the design of the application. 

A more experienced developer can look at all the test cases and make design decisions before start coding, saving a lot of time. 

In this case, for the first iteration I knew the following:
- I need to create a Program class to run the console application (`Main`). Adding all logic there, however simple, is not very testable, so I need to create a separated class `MoneyFormatter.cs`
  that contains the formatting logic. Thanks to that I can separate the tests of the formatter from the tests of the actual console app. 
- I need to abstract the console read functionality in order to stub it and test the console app itself, for what I created the simple `ConsoleAmountRetriever.cs`.
- The Console.ReadLine() returns a string, which means I need to transform to Decimal so that I can use its rounding capabilities.
- 





