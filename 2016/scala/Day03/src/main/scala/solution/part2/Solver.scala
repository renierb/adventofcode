package solution.part2

class Solver(input: Seq[Array[Int]]) {
  def solve: Int = {
    val triangles =
      for {
        g <- input.grouped(3)
        t <- g.flatMap(_.zipWithIndex).groupBy(_._2).map(_._2.map(_._1).toArray)
      } yield t

    triangles.count(t => isValidTriangle(t(0), t(1), t(2)))
  }

  def isValidTriangle(a: Int, b: Int, c: Int): Boolean = {
    if (a + b > c && a + c > b && b + c > a)
      true
    else
      false
  }
}
