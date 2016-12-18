import part1.InputParser

object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines.mkString

  object Part1 extends part1.Solver {
    val floors = InputParser(input)
    override val startState = Elevator(0, floors.sortBy(_.nr).map(f => f.items.toSet))
  }

  val answer1 = Part1.solution
  println(s"Part1: ${answer1.length}")
}
