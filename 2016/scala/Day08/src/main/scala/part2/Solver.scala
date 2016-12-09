package part2

import scala.collection.immutable.{Iterable, SortedMap}

class Solver(input: Stream[Array[String]], override val width: Int = 50, override val height: Int = 6)
  extends part1.Solver(input, width, height) {

  def printScreen(screen: Screen): String = {
    val rows = screen.flatten.groupBy(_.y % height).toSeq

    val lines: Iterable[String] = SortedMap(rows: _*)
      .map {
        case (_, ps) => ps.sortBy(_.x % width).map { p =>
          if (p.on) '#' else '.'
        }.mkString
      }

    lines.mkString("\n")
  }
}
