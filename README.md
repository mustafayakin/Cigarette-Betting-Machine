# Cigarette Betting Machine

Her şeyden önce bu uygulama tamamen **zevkine** yapılmıştır. Herhangi bir bahis iddiası bulunmamaktadır. Bu uygulamayla öğrenmek istediğim şey aslında bu tür uygulamalarla kullanıcının zaman zaman kazanacağı fakat günün sonunda kasanın kâr edebileceği bir sistem yazıp yazamayacağımı görmekti. Bu uygulamada bunu elimden geldiğince test ettim. Örnek aldığım makine ise aşağıdaki gifte görüleceği üzere basit bir mantıkla çalışıyor.

<img src="https://github.com/mustafayakin/Cigarette-Betting-Machine/blob/main/GIFs/mainMachine.gif" width="500" height="400" />


# Bu Makinelerin Mantığı

Kullanıcı **7 adet** sayı belirliyor ve makineye jeton atıyor, daha sonra ortada dönen sayı sizin belirlediğiniz 7 sayıdan birine eşitse makine size bir paket sigara veriyor. Ne kadar masum görünse de bu makinelerin arka planında matematiksel işlemler döndüğünü hepimiz biliyoruz. Bu matematiksel işlemleri kendim basit olarak şöyle oluşturdum.

# Ben Nasıl Uyguladım?

Öncelikle bu makineminiz günlük bir maliyeti olacaktır, *-elektrik, bakım onarım vs-* bu maliyeti makineyi ilk çalıştırdığımızda çıkartmak için ilk başlarda oynayan oyunculara sigara vermiyoruz.

Bu oyunun ücretine bağlı olarak 4-5 kere tekrar ediyor benim uygulamamda *-eğer kullanıcı isterse gamePrice ve dailyExpense değişkenlerini değiştirebilir-*

Makinenin günlük maliyeti çıkarıldıktan sonra matematiksel işlemlere başlıyoruz. Burada bunu yapma amacım **sürekli sigara vermeyen bir makineye kimse para atmak istemez**, biz de kullanıcıları gaza getirmek ve kendi kârımızı da bir yanda korumak için böyle yöntemlere başvururuz.

<img src="https://github.com/mustafayakin/Cigarette-Betting-Machine/blob/main/GIFs/myMachine.gif" width="600" height="500"/>

## Makine Kârını Nasıl Belirledim?

Makinenin günlük maliyeti çıktıktan sonra, kullanıcıya bir ihtimal bloğuna sokuyorum. **3 farklı durumumuz** var bu yüzden bu 3 ihtimalden birine girme olasılığı **%33**, ihtimaller ise şöyle;

 1. Bu bloğa girilirse her el belirlediğim balance üzerinden kasa o el kendi kârını çıkarmak için uğraşır, o el kasa kârını çıkardığında %50 ihtimalle kullanıcıya sigara verir. Yani kullanıcının bu ihtimalde sigara kazanma olasılığı **%16** olur.
 2. Bu bloğa girmesi kullanıcı için olabilecek en kötü senaryodur. Çünkü bu blokta kasa toplam kârını çıkartmak için sürekli bir şekilde sigara vermez. Kendi kârını çıkardığında ve oyun ücretinin üstünde bir para kazandığında kullanıcıya sigara verir, diğer durumlarda kullanıcıya sigara vermez.
3. Son bloğa girmesi durumunda kullanıcıyı 4 durumlu bir ihtimal dizisi bekler. Kullanıcının bu blokta sigara kazanma olasılığı ise **%8** dir.


Burada diğer bir ihitmalden daha bahsetmem gerekiyor. *gain[5]* dizisi. Bu dizi oyun ilk başladığında ve her sigara verildiğinde yeniden random bir şekilde belirleniyor. Amacı ise o el kasa kaç tl kâr etmek istiyor. Her el başladığında ilk blokta gördüğümüz kâr hesaplamaları bu gain’in içinde bulunan sayılar ile yapılıyor. Eğer makine o el 20 TL kâr elde etmek istiyorsa onu elde edene kadar devam ediyor.

    int[] gain = new int[5] { 1, 5, 10, 20, 40 };


Burada görüldüğü gibi kâr bu dizi içinde rastgele her sigara verildiğinde bir daha belirleniyor. Hesaplama yapılırken: 
*gainRandom = rnd.Next(5);* kullanılarak 
*gain[gainRandom];* 
sayesinde belirleniyor. Ve ne kadar kazanmak istediğimizi sorgularken bu gain’den faydalanıyoruz.

    if(balance < (cigPrice+gain[gainRandom])){ 
    dontGive();}

burada görüldüğü üzere kasa ilk blokta sigaranın maliyeti ve kazanmak istediği parayı çıkartana kadar kullanıcıya sigara vermiyor.
## Peki Nasıl Sigara Vermiyor ?

Sigara nasıl vermiyor kısmını anlatmam gerekirse. Kullanıcıdan **7 adet sayı** alıyorduk eğer kullanıcıya o el sigara vermek istemiyorsak kullanıcının belirlediği sayılara bakıyoruz ve o sayının programda çıkmamasını sağlıyorum. Gerçekçi olması ve kullanıcının oyundan soğumaması için randomGenerator(int[],int) adında bir fonksiyon ile bu sorgulamayı **recursive** olarak sağladım. Sürekli farklı bir sayı çıkacak şekilde oluşturmak gerekiyor ki kullanıcı oyunda hile olduğundan şüphelenmesin. Sigara vermek istediğimde ise kullanıcının belirlediği sayılara bakıyorum ve o sayılardan bir tanesini ekrana yazdırıyor, sigarasını veriyorum :).
## Değişkenlerin Açıklaması

Kullanıcının değiştirebileceği değişkenlerdeki amaçlarımı açıklamak gerekirse;

    int balance = 0;

Her el balance adında bir değişkeni sıfırlıyorum ki o el kâr etmek istediğimde bu değişkene göre değiştirme yapabileyim.

    int gamePrice = 5;

Bir oyunun ücreti

    int dailyExpense = 15;

Günlük makinenin maliyeti kısmı burada yer alıyor

    int finalizedBalance = 0;

Kasanın toplam kazandığı parayı hesaplıyorum

    int cigPrice = 20;

Bir adet sigaranın maliyeti

    int gainRandom;

Bununla uygulama ilk başladığında ve sigara verildiğinde tekrar random bir değer üretip kasanın o el kaç TL kar etmek istediğini belirliyoruz.

    int percentageGain;

3 ihtimalli bloğun random sayısını tutmak için oluşturduğum değişken.

    int givenSmokNum = 0;

Toplam verilen sigara miktarı.

    int[] numbers = new int[15] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };

Makinenin alabileceği ve bizim de belirleyebileceğimiz sayılar.

    int[] gain = new int[5] { 1, 5, 10, 20, 40 };

Kasanın o el istediği kazanç.

    int[] userNumber = new int[7];

Kullanıcının belirlediği sayılar.
## Eksikliklerim

 - Background Worker kullanmamış olmam UI için oldukça büyük bir eksiklik.
 - Bet oranlarında değişiklik kazanma olasılıklarını etkilemeli
# Sonuç

Bu oranlarda gördüğümüz gibi kasanın zarar etme olasılığı çok düşük hatta neredeyse yok denecek durumdadır. Bu tür makinelerde bundan daha düşük oranlarda kazanma olasılığı verildiğini biliyoruz. **%3** ler **%5** ihtimaller havada uçuşuyor biz ise bunun basit bir *replikasını* yaptığımız için oranları biraz daha yüksek tutmaya çalıştık. Burada eksiklik sayabileceğimiz bir durum ise bet’in miktarı arttığında sigara verme olasılıklarında değişiklik yapmamam sayılabilir.

 
