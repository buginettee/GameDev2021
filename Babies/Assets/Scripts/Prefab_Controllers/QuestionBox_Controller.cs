using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBox_Controller : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public GameObject consummablePrefab;
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox;
    public Sprite unusedQuestionBox;
    public Transform topColliderTransform;
    public BoxCollider2D topColliderBoxCollider;

    private bool hit = false;
    
    bool ObjectMovedAndStopped()
    {
        return Mathf.Abs(rigidBody.velocity.magnitude) < 0.1;
    }

    IEnumerator DisableHittable()
    {
        if (!ObjectMovedAndStopped())
        {
            yield return new WaitUntil(() => ObjectMovedAndStopped());
        }

        spriteRenderer.sprite = usedQuestionBox;
        rigidBody.bodyType = RigidbodyType2D.Static;

        this.transform.localPosition = new Vector3(0, 0.5f, 0);
        topColliderBoxCollider.offset = new Vector2(0, -0.5f);
        springJoint.enabled = false;
    }
    

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !hit)
        {
            hit = true;

            // spawn the mushroom prefab slightly above the box
            Instantiate(consummablePrefab, new Vector3(this.transform.position.x, this.transform.position.y + 0.3f, this.transform.position.z), Quaternion.identity);
            spriteRenderer.sprite = usedQuestionBox;
            StartCoroutine(DisableHittable());

        }
    }

    // when restarting
    public void onStart()
    {
        hit = false;
        spriteRenderer.sprite = unusedQuestionBox;
        rigidBody.bodyType = RigidbodyType2D.Dynamic;

        this.transform.localPosition = new Vector3(0, 0.5f, 0);
        topColliderBoxCollider.offset = new Vector2(0, 0);
        springJoint.enabled = true;

    }
}
