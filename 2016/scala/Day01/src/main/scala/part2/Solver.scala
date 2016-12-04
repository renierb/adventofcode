package part2

class Solver(instructions: Array[String]) extends DomainDef {

  def start = Santa(North, Pos(0, 0))

  def solve: Int = {
    val finish = instructions.foldLeft(start)((s, i) => s.move(i))

    val hq = finish.hq.getOrElse(Pos(0, 0))
    hq.x.abs + hq.y.abs
  }
}