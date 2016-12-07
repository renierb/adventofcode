package solution.part2

class Solver(input: Stream[String]) {

  def solve = {
    input.foldLeft(0) { (total, s) =>
      if (supportsSSL(s))
        total + 1
      else
        total
    }
  }

  def supportsSSL(ip: String): Boolean = {
    val parts: Stream[(String, Int)] = ip.split(Array('[', ']')).zipWithIndex.toStream

    (for {
      (s, _) <- parts.filter(_._2 % 2 == 0) // supernet, i.e. outside square brackets
      i <- 0 to (s.length - 3) by 1
      if s(i) != s(i + 1) && s(i) == s(i + 2)

      (h, _) <- parts.filter(_._2 % 2 != 0) // hypernet, i.e. inside square brackets
      j <- 0 to (h.length - 3) by 1
      if h(j) != h(j + 1) && h(j) == h(j + 2)

      if s(i) == h(j + 1) && h(j) == s(i + 1)
    } yield true).nonEmpty
  }
}
