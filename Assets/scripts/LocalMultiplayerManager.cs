using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class LocalMultiplayerManager : MonoBehaviour
{
    public List<Sprite> playerVisuals;
    public List<PlayerInput> existingPlayers;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPlayerJoined(PlayerInput newPlayer)
    {
        //assign visuals to this new player
        SpriteRenderer newPlayerRenderer = newPlayer.GetComponent<SpriteRenderer>();
        newPlayerRenderer.sprite = playerVisuals[existingPlayers.Count];
        existingPlayers.Add(newPlayer);

        LocalMultiPlayer playerScript = newPlayer.GetComponent<LocalMultiPlayer>();
        playerScript.manager = this;

    }

    public void TryAttack(PlayerInput attackingPlayer)
    {
        for (int i = 0; i < existingPlayers.Count; i++)
        {
            if (attackingPlayer == existingPlayers[i])
            {
                //skip to next iteration of loop
                continue;
            }

            float distanceToPlayer = Vector3.Distance(attackingPlayer.transform.position, existingPlayers[i].transform.position);

            if (distanceToPlayer < 1.5f)
            {
                Debug.Log("Attacking " + i);
            }
        }
    }



}
