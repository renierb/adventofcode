package part2

import scala.annotation.tailrec
import scala.collection.immutable.SortedSet

class Solver(input: SortedSet[(Long, Long)]) {

  type IpRange = (Long, Long)
  val max: Long = 4294967295L

  def solve: Long = {
    explore(input)
  }

  @tailrec
  private def explore(remainder: SortedSet[(Long, Long)], acc: Long = 0): Long = {
    if (remainder.isEmpty)
      acc
    else {
      val ip = remainder.head._2 + 1
      if (input.exists(p => p._1 <= ip && p._2 >= ip))
        explore(remainder.tail, acc)
      else {
        val count = stream(ip, max).takeWhile(n => !input.exists(p => p._1 <= n && p._2 >= n)).length
        explore(remainder.tail, acc + count)
      }
    }
  }

  def stream(from: Long, to: Long): Stream[Long] =
    if (from > to)
      Stream.empty
    else
      from #:: stream(from + 1, to)
}
