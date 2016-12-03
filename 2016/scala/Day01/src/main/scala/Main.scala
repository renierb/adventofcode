
object Main extends App {
  val input = io.Source.fromInputStream(getClass.getClassLoader.getResourceAsStream("day1.txt")).getLines.mkString

  val answer = new Day01(input.split(',').map(_.trim)).solve
  println(s"$answer blocks away")
}