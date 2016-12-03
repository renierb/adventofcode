
class Day01(instructions: Array[String]) extends GameDef {

  def start = Santa(North, Pos(0, 0))

  def solve: Int = {
    val finish = instructions.foldLeft(start)((s, i) => s.move(i))
    finish.pos.x.abs + finish.pos.y.abs
  }
}