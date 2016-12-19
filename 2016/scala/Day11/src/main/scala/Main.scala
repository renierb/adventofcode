import part1.InputParser

object Main extends App {
  val input1 = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input1.txt")).getLines.mkString

  object Part1 extends part1.Solver {
    val floors = InputParser(input1)
    override val startState = Elevator(0, floors.sortBy(_.nr).map(f => f.items.toSet).toVector)
  }

  val answer1 = Part1.solve
  println(s"Part1: $answer1")

  val input2 = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input2.txt")).getLines.mkString

  object Part2 extends part1.Solver {
    val floors = InputParser(input2)
    override val startState = Elevator(0, floors.sortBy(_.nr).map(f => f.items.toSet).toVector)
  }

  val answer2 = Part2.solve
  println(s"Part2: $answer2")
}
