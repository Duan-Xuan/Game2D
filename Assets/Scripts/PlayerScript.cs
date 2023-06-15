using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float speedX, speedY;//tốc độ theo trục x,y
    private Animator player;
    private bool isDanh, isBoss, isBullet;
    public GameObject die;
    private GameObject dieBoss;
    public static Slider slider, slider2;
    private Image image, image2;
    int demCoin = 0, demRuby = 0;
    private Text coin, ruby;
    public AudioClip coinClip, fightClip;
    private AudioSource audioSource;
    public static Vector3 _Transform;
    private Rigidbody2D obs;
    private BoxCollider2D obs2;
    
    IEnumerator LoadB1(float delay)
    {
        yield return new WaitForSeconds(delay);
        UnityEngine.SceneManagement.SceneManager.LoadScene("You-win");
    }
    
    IEnumerator LoadB2(float delay)
    {
        yield return new WaitForSeconds(delay);
        obs.velocity = new Vector2(transform.localScale.x, 0) * 5.5f;
        StartCoroutine(LoadB3(0.5f));
    }
    
    IEnumerator LoadB3(float delay)
    {
        yield return new WaitForSeconds(delay);
        obs.velocity = new Vector2(0, 0);
        obs.gravityScale = 1;
        obs2.isTrigger = false;
    }
    
    IEnumerator Bullet(float delay)
    {
        yield return new WaitForSeconds(delay);
        isBullet = !isBullet;
    }

    void Start()
    {
        obs = gameObject.GetComponent<Rigidbody2D>(); //anh xa
        obs2 = gameObject.GetComponent<BoxCollider2D>(); //anh xa
        player = GetComponent<Animator>();
        slider = GameObject.FindWithTag("Slider").GetComponent<Slider>();
        slider2 = GameObject.FindWithTag("Slider2").GetComponent<Slider>();
        image = GameObject.FindWithTag("Fill").GetComponent<Image>();
        image2 = GameObject.FindWithTag("Fill2").GetComponent<Image>();
        coin = GameObject.Find("Text(coin)").GetComponent<Text>();
        ruby = GameObject.Find("Text(ruby)").GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
        //hiển thị trạng thái;
        player.SetBool("isIdie", true);
        player.SetBool("isRun", false);
        player.SetBool("isAttack", false);
        player.SetBool("isJump", false);
        player.SetBool("isDead", false);
        obs = gameObject.GetComponent<Rigidbody2D>(); //anh xa
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Boss")
        {
            slider.value -= 20;
            slider2.value -= 20;
            if (slider.value <=0)
            {
                Application.LoadLevel("You-loss");
            }
        }
        if (collision.gameObject.tag == "Boss_Boss")
        {
            slider.value -= 40;
            slider2.value -= 40;
            if (slider.value <=0)
            {
                Application.LoadLevel("You-loss");
            }
        }
        if (collision.gameObject.tag == "Coin")
        {
            demCoin++;
            coin.text = 'X' + demCoin.ToString();
            audioSource.PlayOneShot(coinClip);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Ruby")
        {
            demRuby++;
            ruby.text = 'X' + demRuby.ToString();
            audioSource.PlayOneShot(coinClip);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "matsan")
        {
            StartCoroutine(LoadB2(0.5f));
            obs.velocity = new Vector2(0, transform.localScale.y) * 3.5f;
            obs.gravityScale = 0;
            obs2.isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Boss")
        {
            isDanh = !isDanh;
            dieBoss = collider.gameObject;
        }
        
        if (collider.gameObject.tag == "Boss_Boss")
        {
            isBoss = !isBoss;
            dieBoss = collider.gameObject;
        }
    }
 
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))//nhan mui ten trai
        {
            //hiển thị trạng thái;
            player.SetBool("isIdie", false);
            player.SetBool("isRun", true);
            player.SetBool("isAttack", false);
            player.SetBool("isJump", false);
            //di chuyen
            gameObject.transform.Translate(Vector2.right * speedX * Time.deltaTime);
            //quay dau neu nguoc chieu
            if (gameObject.transform.localScale.x < 0)
            {
                gameObject.transform.localScale
                    = new Vector2(gameObject.transform.localScale.x * -1,
                        gameObject.transform.localScale.y);
            }
        }
        else if (Input.GetKey(KeyCode.LeftArrow))//nhan mui ten phai
        {
            //hiển thị trạng thái;
            player.SetBool("isIdie", false);
            player.SetBool("isRun", true);
            player.SetBool("isAttack", false);
            player.SetBool("isJump", false);
            //di chuyen
            gameObject.transform.Translate(Vector2.left * speedX * Time.deltaTime);
            
            //quay dau neu nguoc chieu
            if(gameObject.transform.localScale.x >0)
            {
                gameObject.transform.localScale
                    = new Vector2(gameObject.transform.localScale.x * -1,
                        gameObject.transform.localScale.y);
            }
        }
        else if(Input.GetKey(KeyCode.UpArrow))//nhan mui ten
        {
            //hiển thị trạng thái;
            player.SetBool("isIdie", false);
            player.SetBool("isRun", false);
            player.SetBool("isAttack", false);
            player.SetBool("isJump", true);
            //nhan vat bay
            //neu muon nhay-> can dieu kien kiem tra mat san
            //if(gameObject.tag=="matsan")
            gameObject.GetComponent<Rigidbody2D>().velocity
                = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x,
                    speedY);
        }
        else if(Input.GetKey(KeyCode.Space))//nhan dau space
        {
            //Âm thanh
            audioSource.PlayOneShot(fightClip);
            //hiển thị trạng thái;
            player.SetBool("isIdie", false);
            player.SetBool("isRun", false);
            player.SetBool("isAttack", true);
            player.SetBool("isJump", false);
            if (isDanh)
            {
                isDanh = !isDanh;
                GameObject boss = Instantiate(die, gameObject.transform.position, Quaternion.identity);
                Destroy(boss, 1);
                Destroy(dieBoss);
            }

            if (isBoss)
            {
                isBoss = !isBoss;
                GameObject boss = Instantiate(die, gameObject.transform.position, Quaternion.identity);
                Destroy(boss, 2);
                Destroy(dieBoss);
                StartCoroutine(LoadB1(2f));
            }
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            slider.value = slider.value / 2;
            slider2.value = slider.value / 2;
        }
        else
        {
            //hiển thị trạng thái;
            player.SetBool("isIdie", true);
            player.SetBool("isRun", false);
            player.SetBool("isAttack", false);
            player.SetBool("isJump", false);
        }
        
        //set màu cho thanh máu
        if (slider.value >= 60)
        {
            image.color = Color.green;
            image2.color = Color.green;
        }else if (slider.value >= 30)
        {
            image.color = Color.yellow;
            image2.color = Color.yellow;
        }else
        {
            image.color = Color.red;
            image2.color = Color.red;
        }
    }
}
