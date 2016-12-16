package part2

import scala.annotation.tailrec

class Solver(input: String, length: Int) {

  def solve: String = {
    checksum(transform())
  }

  @tailrec
  final def transform(next: Stream[Char] = Stream(input: _*), acc: Int = input.length): Stream[Char] = {
    if (acc >= length)
      next.take(length)
    else
      transform(next #::: ('0' #:: next.reverse.map(c => if (c == '1') '0' else '1')), acc * 2 + 1)
  }

  @tailrec
  final def checksum(input: Stream[Char] = Stream(input: _*), answer: StringBuilder = new StringBuilder(input.length / 2 + 1)): String = input match {
    case Stream.Empty =>
      if (answer.length % 2 == 1)
        answer.toString
      else
        checksum(Stream(answer: _*), new StringBuilder(answer.length / 2 + 1))

    case h1 #:: h2 #:: tail =>
      if (h1 == h2)
        checksum(tail, answer.append("1"))
      else
        checksum(tail, answer.append("0"))

    case _ #:: tail =>
      checksum(tail, answer)
  }
}
