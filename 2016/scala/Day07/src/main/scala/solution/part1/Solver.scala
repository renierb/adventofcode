package solution.part1

class Solver(input: Stream[String]) {

  def solve = {
    input.foldLeft(0) { (total, s) =>
      if (supportsTLS(s))
        total + 1
      else
        total
    }
  }

  def supportsTLS(ip: String): Boolean = {
    val parts: Array[(String, Int)] = ip.split(Array('[', ']')).zipWithIndex

    val inBrackets = parts.filter(_._2 % 2 != 0).exists { case (s, i) => hasABBA(s) }
    !inBrackets && parts.filter(_._2 % 2 == 0).exists { case (s, i) => hasABBA(s) }
  }

  def hasABBA(s: String): Boolean = {
    (0 to (s.length - 4) by 1).toStream
      .exists { i =>
        (s(i) != s(i + 1)) &&
          ((s(i) == s(i + 3)) && (s(i + 1) == s(i + 2)))
      }
  }
}
