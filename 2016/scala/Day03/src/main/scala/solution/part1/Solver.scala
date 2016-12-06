package solution.part1

class Solver(input: Seq[Array[Int]]) {
  def solve: Int = {
    input.count(t => isValidTriangle(t(0), t(1), t(2)))
  }

  def isValidTriangle(a: Int, b: Int, c: Int): Boolean = {
    if (a + b > c && a + c > b && b + c > a)
      true
    else
      false
  }
}
