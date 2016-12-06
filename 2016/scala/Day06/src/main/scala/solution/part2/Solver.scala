package solution.part2

class Solver(messages: Seq[String]) {

  def solve = {
    val msglen = messages.head.length
    (0 until msglen).foldLeft(List[Char]())((m, i) => messages.groupBy(_(i)).minBy(_._2.length)._1 :: m)
      .reverse
      .mkString
  }
}
