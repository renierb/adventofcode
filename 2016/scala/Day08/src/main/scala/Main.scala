
object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("input.txt")).getLines
  val instructions = input.map(_.split(' ')).toStream

  val solver = new part2.Solver(instructions)
  val screen = solver.solve

  // part 1
  println(s"Pixels lit: ${solver.pixels(screen)}")

  // part 2
  println(solver.printScreen(screen))
}
