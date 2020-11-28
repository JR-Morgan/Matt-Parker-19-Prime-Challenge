## Matt Parker 19 Prime Challenge
This is my code and solutions to the Matt Parker's Maths Puzzle : [The 19 Challenge](https://www.think-maths.co.uk/19challenge)

Let *P* be a set of all prime numbers

Find values of *n ∈ ℤ* such that: 
<img src="https://user-images.githubusercontent.com/45512892/100528301-52af1b00-31d3-11eb-8239-6a544f3d77c9.png" height="50" />
is a multiple of *n*.

In other words, take the first *n* primes. square them, then sum them, and if that value is a multiple  of *n*. Then *n* is a valid solution.
I wanted to find the largest value of n where this property is true.

## My approach

My initial approach was to generate primes using a variation of the Sieve of Eratosthenes, and then square and sum them. This approach quickly allowed me to compute the first 9 solutions, including n= 1 and n=19.
I ran into some interesting problems implementing this. Since the sum was such a large number, I ran out of bits! Even with a 64bit signed integer or a "long", I was quickly getting integer overflows. So I upgraded to a 128 Integer (and then later to a 256bit) Integer using the [BigMath](https://www.nuget.org/packages/BigMath/) package. This allowed me to store the much larger numbers that are required by this challenge.

However, generating primes this way was incredibly slow, so my options were to either optimise the way I was generating primes, or find a list of primes online. After tinkering around with my prime sieve, I reluctantly gave up, and started a search for a list of primes.
Searching the internet, there were many data sets for the first 50,000,000 or so primes. But I knew that I needed many, many more.
I then found this list of primes http://www.primos.mat.br/indexen.html

It seemed that they had exactly what I was after, 454GB(uncompressed) of primes, 38 billion in total. I cannot over-express the joy I felt when I discovered this.
So after modifying my code to read these text files and implement a serialisation (saving) function so that I could stop and start my program, I crunched through the first 2 Billion primes (only a fraction the total data set). The first 9 solutions are all reasonably close together and even my previous program could find them basically instantly. However the next 2 solutions would take about 30 mins to find. 

The whole prime data set of 38 billion primes took in total 4 hours to run through. And note that I didn't find any more solutions than I did running through the first 2 billion.

## Solutions

| n | Last Prime | Sum |
| -- | -- | -- |
| 1 | 2 | 4 |
| 19 | 67 | 24966 |
| 37 | 157 | 263736 |
| 455 | 3217 | 1401992410 |
| 509 | 3637 | 2040870112 |
| 575 | 4201 | 3054955450 |
| 20597 | 231947 | 346739122490032 |
| 202717 | 2790569 | 499159078330000800 |
| 1864637 | 30116309 | 539391065522650998496 |
| 542474231 | 12021325961 | 25318239660367402306502991202 |
| 1139733677 | 26144296151 | 251882074412384639674100925616 |


## To run
First you require the list of primes available at http://www.primos.mat.br/indexen.html

The full data set contains primes with values between 0 - 1 trillion which is 37,607,912,018 primes in total.

The full data set is 46.7GB and decompresses to a size of 454GB.

You can download a subset of the data set and run through it.
Place this data set in a the root directory of the downloaded program, in a folder called `Primes`.
Alternatively you can specify the primes path. See program arguments.
At the end of every file the program will save a `State.txt` file which saves it's progress, meaning it can pick up where it left off seamlessly.


#### Program arguments
| Index | Description |
| -- | -- |
| 0 | Number of groups to run over. (default `10`) |
| 1 | Path to write the `Solutions.txt`. (default `Solutions.txt`) |
| 2 | Path to read/write the `State.txt` file. (default `State.txt`) |
| 3 | Path of the Primes data set. (default `Primes\`) |
| 4 | The file type of the primes data. (default `.txt`) |

