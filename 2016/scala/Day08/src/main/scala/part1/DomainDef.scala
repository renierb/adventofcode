package part1

trait DomainDef {

  val width: Int = 50
  val height: Int = 6

  case class Pixel(y: Int, x: Int, on: Boolean = false)
  type Screen = Array[Array[Pixel]]

  case class Rect(screen: Screen) {
    def apply(w: Int, h: Int): Screen = {
      screen.map { row =>
        row.map { p =>
          if (p.x % width < w && p.y % height < h)
            p.copy(on = true)
          else
            p
        }
      }
    }
  }

  case class RowAction(screen: Screen) {
    def right(y: Int, by: Int): Screen = {
      screen.map { row =>
        row.map { p =>
          if (p.y % height == y)
            p.copy(x = p.x + by)
          else
            p
        }
      }
    }
  }

  case class ColAction(screen: Screen) {
    def down(x: Int, by: Int): Screen = {
      screen.map { row =>
        row.map { p =>
          if (p.x % width == x)
            p.copy(y = p.y + by)
          else
            p
        }
      }
    }
  }
}
