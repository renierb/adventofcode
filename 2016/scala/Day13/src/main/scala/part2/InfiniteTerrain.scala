package part2

trait InfiniteTerrain extends DomainDef {
  val magicNumber: Int

  val terrain: Terrain = (pos: Pos) => {
    val x = pos.x
    val y = pos.y

    val isValid = x >= 0 && y >= 0 &&
                  (countBits(x*x + 3*x + 2*x*y + y + y*y + magicNumber) % 2) == 0
    isValid
  }

  def countBits(n: Int): Int = {
    var i = n - ((n >> 1) & 0x55555555)
    i = (i & 0x33333333) + ((i >> 2) & 0x33333333)
    (((i + (i >> 4)) & 0x0F0F0F0F) * 0x01010101) >> 24
  }
}
