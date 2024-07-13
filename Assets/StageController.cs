using UnityEngine;

public class StageController : MonoBehaviour
{
    public GameObject Singer;
    public GameObject Guitarist;
    public GameObject Bassist;
    public GameObject Drummer;

    private Vector3 singerStartPos;
    private Vector3 guitaristStartPos;
    private Vector3 bassistStartPos;
    private Vector3 drummerStartPos;

    private Vector3 singerTargetPos;
    private Vector3 guitaristTargetPos;
    private Vector3 bassistTargetPos;

    private float singerSpeed;
    private float guitaristSpeed;
    private float bassistSpeed;

    private float moveDuration = 2f;
    private float elapsedTime = 0f;
    private bool singerAtMiddle = true;

    void Start()
    {
        singerStartPos = Singer.transform.position;
        guitaristStartPos = Guitarist.transform.position;
        bassistStartPos = Bassist.transform.position;
        drummerStartPos = Drummer.transform.position;

        singerTargetPos = singerStartPos;
        guitaristTargetPos = guitaristStartPos;
        bassistTargetPos = bassistStartPos;

        RandomizeSpeeds();
    }

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= moveDuration)
        {
            elapsedTime = 0f;
            moveDuration = Random.Range(2f, 5f);
            RandomizeSpeeds();

            if (singerAtMiddle)
            {
                singerTargetPos = new Vector3(Random.Range(-5f, 5f), singerStartPos.y, singerStartPos.z);
                guitaristTargetPos = guitaristStartPos;
                bassistTargetPos = bassistStartPos;
            }
            else
            {
                singerTargetPos = singerStartPos;
                guitaristTargetPos = new Vector3(singerStartPos.x, guitaristStartPos.y, guitaristStartPos.z);
                bassistTargetPos = new Vector3(singerStartPos.x, bassistStartPos.y, bassistStartPos.z);
            }

            singerAtMiddle = !singerAtMiddle;
        }

        MovePerformer(Singer, singerTargetPos, singerSpeed);
        MovePerformer(Guitarist, guitaristTargetPos, guitaristSpeed);
        MovePerformer(Bassist, bassistTargetPos, bassistSpeed);
    }

    void MovePerformer(GameObject performer, Vector3 targetPos, float speed)
    {
        performer.transform.position = Vector3.MoveTowards(performer.transform.position, targetPos, speed * Time.deltaTime);
    }

    void RandomizeSpeeds()
    {
        singerSpeed = Random.Range(1f, 3f);
        guitaristSpeed = Random.Range(0.5f, 2f);
        bassistSpeed = Random.Range(0.5f, 2f);
    }
}

