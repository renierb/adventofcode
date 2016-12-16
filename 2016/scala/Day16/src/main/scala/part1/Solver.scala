package part1

import scala.annotation.tailrec

class Solver(a: String, length: Int) {

  def solve: String = {
    checksum(transform())
  }

  @tailrec
  final def transform(next: String = a): String = {
    if (next.length >= length)
      next.take(length)
    else
      transform(next + "0" + next.reverse.map(c => if (c == '1') 0 else 1).mkString)
  }

  @tailrec
  final def checksum(input: String = a): String = {
    val result = (0 until input.length by 2).foldLeft("") { (s, i) =>
      val taken = input.substring(i, i + 2)
      if (taken.length == 1)
        s
      else
        s + (if (taken(0) == taken(1)) "1" else "0")
    }
    if (result.length % 2 == 1)
      result
    else
      checksum(result)
  }
}
