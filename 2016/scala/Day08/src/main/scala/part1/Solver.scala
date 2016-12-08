package part1

class Solver(input: Stream[Array[String]], override val width: Int = 50, override val height: Int = 6) extends DomainDef {

  val screen = Array.fill[Pixel](height, width)(Pixel(-1, -1))
  val indexes = for {
    row <- 0 until height
    col <- 0 until width
  } yield (row, col)

  indexes.foreach { case (r, c) =>
    screen(r)(c) = Pixel(r, c)
  }

  def solve: Int = {
    val lastScreen = input.foldLeft(screen) { case (s, i) =>
      i(0) match {
        case "rect" =>
          val dim = i(1).split('x').map(_.toInt)
          Rect(s)(dim(0), dim(1))
        case "rotate" =>
          i(1) match {
            case "row" =>
              val y = i(2).split('=')(1).toInt
              val by = i(4).toInt
              RowAction(s).right(y, by)
            case "column" =>
              val x = i(2).split('=')(1).toInt
              val by = i(4).toInt
              ColAction(s).down(x, by)
          }
      }
    }
    lastScreen.flatten.count(_.on)
  }
}
