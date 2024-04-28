﻿namespace MultiShop.Discount.Dtos
{
    public class CreateDiscountCouponDto
    {
        /// <summary>
        ///     Kupona ait kod bilgisidir.
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        ///     İndirim oranı tutulur.
        /// </summary>
        public int Rate { get; set; }
        /// <summary>
        ///     Kuponun aktif mi bilgisidir.
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        ///     Kuponun son geçerlilik tarihi bilgisidir.
        /// </summary>
        public DateTime ValidDate { get; set; }
    }
}
