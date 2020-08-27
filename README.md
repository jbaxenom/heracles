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


## Programming Language and Framework
I have used many programming languages in the past (Java, JS, Ruby, Python... Even Erlang), but once I became a team manager the amount
of time I could dedicate to code got reduced drastically. In my last company I started coding much more, this time in C#. I decided to 
be pragmatic and code the application in it, to be able to use the limitted time available to show how I design, code and 
test, rather than refreshing programming languages I've not used in several years.

In order to make the solution runable in any OS, and not to limit potential improvements later, I decided to use .NET Core 3.1.X. 

## Delivery
I chose to try GitHub Actions for CI. I had not used it before but I'm quite familiar with CI tools so it made sense to use the one
provided with GitHub, which was the repository hosting service requested by Pleo. 

As I could not know how much time I was going to be able to put on the exercise, I chose to follow an iterative approach to how the 
application would work. The idea is to increase the complexity of the architecture in each iteration, based on the time I have. The
iterations are as follow:

- **DROP 1**: Console application, delivered as a project that can be built for Linux, Windows or Mac (through dotnet build)
- **DROP 2**: Webservice, providing an external API that has the same functionality as the Console application. Provided as a docker container.
- **DROP 3**: Web Application, a simple HTML frontend that uses the Webservice created in iteration 2. Solution provided as docker-compose containers.

## Methodology
As the specification is given by example, I decided to implement it using TDD. The steps are as follow:
1. Define the User-level AC
2. Break down the AC into UT cases
3. Implement a UT case
4. See the UT case fail
5. Implement the functionality to make the test pass
6. See the test pass
7. Refactor (if needed)
8. Iterate from 3 with a new test case

### User-level AC

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

#UAC6
Given the user is prompted with entering an amount
When they input a very big amount
Then the Heracles formatter responds without error
```

There are multiple considerations around the behavior of the application that cannot be inferred from the Examples provided or the 
description of the exercise. Some examples are:

- How should Heracles ask for the amount? Which text? Which format?
- How does the application behave after providing the format transformation?
  - Does it terminate after showing the response? 
  - Does it restart immediatedly instead?\
  - Perhaps it asks the user what to do? How?

In order to finish the implementation I took some assumptions based on the above (normally I would have a PO to ask these questions):
- I provided some basic messages for the question and answers
- The application will:
  - Terminate after pressing a key in DROP 1
  - Stay running waiting for more calls in DROP 2
  - Reload the form in DROP 3

### 2. Breaking AC into Test Cases

Let's start with UAC1. From the format conversion we can infer the following:

```
TC1: heracles must prompt the user to enter a money amount and capture it
TC2: given an amount input, heracles should display it as a string with the heracles format
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

From UAC3-6 we can add:

```
TC4: if the input is a negative number, heracles should show an error with text 'You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number of up to 29 digits'
TC5: if the input contains a non-digit character, heracles should show an error with text 'You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number of up to 29 digits'
TC6: if the input is an empty space, heracles should show an error with text 'You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number of up to 29 digits'
TC7: if the input is a number above 29 digits, heracles should show an error with text 'You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number of up to 29 digits'
TC8: if the input is empty (new line), heracles should show an error with text 'You have entered an invalid amount. The amount must be a positive, decimal or non-decimal number of up to 29 digits'
```


### 3. Implement a Unit Test
These TCs will be implemented as Unit Test, shaping the functionality and the design of the application. 

A more experienced developer can look at the test cases as a whole and make design decisions before starting to code, saving a lot of time. For example, for the first iteration of Heracles I knew the following:
- I need to create a Program class to run the console application (`Main`).
- Adding all logic there, however simple, is not very testable, so I need to create a separated class `MoneyFormatter.cs`
  that contains the formatting logic. Thanks to that I can separate the tests of the formatter from the tests of the actual console app. 
- The Console.ReadLine() returns a string, which means I need to transform to double to use its formatting capabilities.

There are 2 libraries I always use for test automation: a fluent assertion library and a mock library. The first is important
to make the tests as readable as possible. The second is obviously needed to separate concerns and test components individually. In C# the best
libraries for these tasks are `FluentAssertions` and `Moq`, respectively.

The choice of test framework is pretty much up to taste. In C# I use `MStest` because it integrates nicely with Azure DevOps test management module, linking test methods to the respective "test case task" and automatically
run them by test plan.   

In addition, one of the best complementary techniques to improve over TDD -and to develop in general- is the use of *Mutation Testing* frameworks. Mutation testing
introduces bugs in the code (mutants) and checks if your Unit Tests catch them. One of the best mutation testing tools for C# is `Stryker.NET`. Its reports help not only
ensuring your UTs are optimal, but also make your code better by ensuring you follow best practices in making code robust against bugs. 

Once the tools are chosen and dependencies installed, we can start writing a test. I use the _Arrange_, _Act_ and _Assert_ paradigm, which is pretty much like Guerkin's _Given_, _When_, _Then_. It makes tests systematic and 
easy to read: we set up test data and/or stubs in _Arrange_, we run the SUT in _Act_ and then we _Assert_ that the actual behavior is the expected one.  

### 4. See the UT case fail
In TDD, the SUT is being developed after the test, which means that we can only build skeletons of it and then fill them up with functionality as we 
go through the different test cases. Running the UT and seeing it fail -not crash- is important to make sure we're not missing any check.

### 5. Implement the functionality to make the test pass
If following the steps properly you'll realise this one gets quite simplified vs the classic "big-bang" approach: you need to focus on implementing the part that makes the test pass. If too much
logic falls into it, most probably it can be broken down and more tests should be added. Code will be constantly refactored and changed, which is fine although it feels like a waste of time. In fact it is not, as
the design is done to favour reliability and robustness, one piece at a time.

Sometimes several use cases can be implemented in one go, for example when programming frameworks help handling multiple things in one instruction. As you can see this is the case for some test cases in this implementation, 
as the `double.ToString("<format>")` formatter does most of the work needed to implement the requirements.

### 6. See the test pass
In here is where I'll also add the Stryker.NET run, because I want to make sure my tests and code are robust. I always get the feeling my code gets much better after using Stryker. 

### 7. Refactor (if needed)
Most probably it will be needed, as functionality is added independently and often can be joined or abstracted. If not done in step 5, now is the time before it gets too clunky.

### 8. Repeat from step 3
The whole exercise has been done following the steps above, so there was a lot of repeating!

## DROP 1
Drop 1 is a console application built using TDD methodology that implements the Heracles specs. It consists of the `heracles.console` and `heracles.unit.tests` projects. 

## DROP 2
Drop 2 is a web app built on top of Drop 1. It has been created using Visual Studio's ASP.NET Core template, adding the logic in `MoneyFormatter.cs` from DROP 1 as the `MoneyController.cs`
It also includes:
- A [SwaggerUI interface](http://localhost:32786/swagger),
- An auto-generated Heracles http client using the swagger.json contract, built with [AutoRest](https://github.com/Azure/autorest).
- A docker wrapper to run it locally easily
- Unit Tests
- Integration tests with configurable endpoint, pointing now to the docker instance. 

Drop 2 has not been built using TDD, as most of the code was pre-generated. As most of the logic is unit tested the coverage at this level is lower. While more tests could be added, I left them to a minimum due to time constraints. 
I'm quite happy with the overall coverage, though :).

## Usage
- DROP 1: Download the binaries for your OS, decompress them and run the "heracles" binary (you might need to 'chmod +x').\
- DROP 2: 
  - Download the [source code](https://github.com/jbaxenom/heracles)
  - Build the docker image (`docker build . --file src/heracles.webapp/Dockerfile`)
  - Run the image (`docker run -t -d --name heracles -p 32786:80 heracles`)
  - Open http://localhost:32786/swagger and let it instruct you on how to use the endpoint :)





