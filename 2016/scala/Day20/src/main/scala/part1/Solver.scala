package part1

import scala.annotation.tailrec
import scala.collection.immutable.SortedSet

class Solver(input: SortedSet[(Long, Long)]) {

  type IpRange = (Long, Long)
  val max: Long = 4294967295L

  def solve: Long = {
    explore(input).getOrElse(-1L)
  }

  @tailrec
  private def explore(remainder: SortedSet[(Long, Long)]): Option[Long] = {
    if (remainder.isEmpty)
      None
    else {
      val ip = remainder.head._2 + 1
      if (input.exists(p => p._1 <= ip && p._2 >= ip))
        explore(remainder.tail)
      else
        Some(ip)
    }
  }
}
