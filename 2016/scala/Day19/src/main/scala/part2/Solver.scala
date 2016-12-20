package part2

import scala.annotation.tailrec
import scala.collection.mutable.ArrayBuffer

class Solver(total: Int) {

  private val presents = ArrayBuffer.fill[Int](total)(1).zipWithIndex

  def solve: Int = {
    takePresents(0, total)
  }

  @tailrec
  final def takePresents(taker: Int, elves: Int = total): Int = {
    require(elves >= 1)
    if (elves == 1)
      presents(0)._2 + 1
    else {
      val relative = (180.0 / (360.0 / elves)).toInt
      var takee = (taker + relative) % elves
      presents.remove(takee)

      takePresents((taker + 1) % total, elves - 1)
    }
  }
}